using HashStrike.Client;
using System;
using System.Text;
using System.Threading;
public static class Bruteforcer
{
    static ServerClient serverClient = new ServerClient();

    public static void StartBruteForce(HashTask task, CancellationToken cancellationToken)
    {
        char[] symbols = GetSymbols(task.HasUpperCase, task.HasLowerCase, task.HasDigits, task.HasSymbols);
        string currentString = task.StartString;
        string endString = task.EndString;

        while (true)
        {
            // Проверяем, была ли запрошена отмена
            if (cancellationToken.IsCancellationRequested)
            {
                // Можно выполнить дополнительные действия при отмене, если нужно
                return; // Завершаем выполнение метода
            }

            if (IsEndString(currentString, endString)) break;

            string localCurrentString = currentString; // создаем локальную переменную для захвата текущей строки
            string hash = task.HashStrategy.ComputeHash(localCurrentString);

            // Проверка совпадения хеша
            if (hash.Equals(task.TargetHash, StringComparison.OrdinalIgnoreCase))
            {
                serverClient.SendResultAsync(Program.hostName, localCurrentString);
                // Если нашли совпадение, можно завершить процесс
                return;
            }

            // Обновление текущей строки в консоли
            Console.SetCursorPosition(0, Console.CursorTop); // Сброс курсора на начало строки
            Console.Write($"Hashing: {currentString}     "); // Вывод текущей строки
            Console.CursorLeft = 0; // Сброс курсора, чтобы не смещать следующую строку

            currentString = GetNextString(currentString, symbols);
            if (currentString.Length > endString.Length && string.Compare(currentString.Substring(0, endString.Length), endString) > 0)
            {
                break;
            }
        }

        // Если ни одной строки не удалось подобрать, отправляем ложный результат
        serverClient.SendResultAsync(Program.hostName, "false");
    }

    static bool IsEndString(string current, string end)
    {
        return current == end;
    }

    static char[] GetSymbols(bool upper, bool lower, bool digits, bool symbolsAllowed)
    {
        StringBuilder symbolSet = new StringBuilder();
        if (digits) symbolSet.Append("0123456789");
        if (lower) symbolSet.Append("abcdefghijklmnopqrstuvwxyz");
        if (upper) symbolSet.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        if (symbolsAllowed) symbolSet.Append("!@#$%^&*()_-+=<>?/[]{}|;:'\",.");

        return symbolSet.ToString().ToCharArray();
    }

    static string GetNextString(string current, char[] symbols)
    {
        char[] chars = current.ToCharArray();
        for (int i = chars.Length - 1; i >= 0; i--)
        {
            int index = Array.IndexOf(symbols, chars[i]) + 1;

            if (index < symbols.Length)
            {
                chars[i] = symbols[index];
                return new string(chars);
            }
            else
            {
                chars[i] = symbols[0]; // сброс символа на первый
            }
        }
        // Если перебрали все символы, добавляем новый символ
        return new string(symbols[0], chars.Length + 1);
    }
}
