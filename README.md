# Распределенная система для брутфорса хеш-функций

Данное приложение представляет собой распределенную систему для брутфорса хеш-функций. Каждый хост, на котором запущено клиентское приложение, периодически обращается к API сервера и получает свой диапазон значений для брутфорса хеш-функции. В данный момент поддерживаются алгоритмы хеширования MD5 и SHA256.

## Описание системы

Система позволяет бесшовно передавать задания в случае отказа одних хостов другим доступным хостам. Сервер выступает в роли менеджера заданий и распределителя диапазонов значений для отдельных хостов. Задания, заданные администратором, хранятся в базе данных в очереди и распределяются по активным хостам друг за другом.

При первом подключении клиентского приложения хосту выдается персональный идентификатор, и он регистрируется в базе на сервере. Задания для перебора хеша разделяются поровну для всех активных хостов в системе, без учета их вычислительной способности. 

Предусмотрено управление задачами с помощью Telegram-бота. Демонстрацию работы системы можно посмотреть по следующей ссылке: [Демонстрация работы](https://watch.wave.video/yKpQXdHxSrC79tv4).

Таким образом, можно объединять вычислительные мощности устройств для работы над одной задачей. В дальнейшем система также может быть использована для создания пула машин для майнинга криптовалют.

## Используемые технологии

- MS SQL
- C#
- Entity Framework
- Newtonsoft.Json
- Web API

## Требования для запуска приложения

- Visual Studio 2019 или выше
- MS SQL Server
- Telegram клиент (Desktop)

## Установка и запуск

1. Клонируйте репозиторий.
2. Откройте решение в Visual Studio.
3. Настройте подключение к базе данных MS SQL (можно сформировать базу по моделям, имеющимся в проекте).
4. Замените доменное имя на ваше доменное имя во всех сборках, если это требуется.
5. Замените токен Telegram-бота на ваш токен, предварительно создав бота с помощью BotFather. Токен бота хранится в сборке HashStrike.Bot, папка Models, класс CommonVariables.
6. Запустите API.
7. Запустите приложение бота.
8. Наконец, запустите клиентские приложения.

Сформируйте задание с помощью Telegram-бота, следуя шаг за шагом отвечая на вопросы бота. После того, как система приняла задание, вам остается ожидать ответа — либо подобранное значение для хеш-функции, либо неудача после полного перебора по диапазону с параметрами, которые вы задали при формировании задачи.
