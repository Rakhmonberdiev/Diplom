# Ride-ticket

Краткое описание: Веб-приложение для бронирования билетов на маршрутки с функциями регистрации, авторизации, поиска и бронирования билетов, генерации QR-кода и просмотра истории бронирования.

## Фронтенд
[Фронтенд](https://github.com/Rakhmonberdiev/DiplomClientApp)

## Технологии

- .NET Core
- Entity Framework Core
- ASP.NET Core
- PostgreSQL (в качестве базы данных)
- JWT для аутентификации
- Хеширование паролей

## Установка и запуск

1. Клонируйте репозиторий.
2. Установите .NET Core SDK, если требуется.
3. Убедитесь, что у вас установлен PostgreSQL и настроено подключение в файле `appsettings.json`.
4. Перейдите в папку проекта и выполните `dotnet restore`.
5. Выполните миграции базы данных с помощью `dotnet ef database update`.
6. Запустите приложение с помощью `dotnet run`.
7. Приложение будет доступно по адресу `http://localhost:5000`.

## Основные функции

- Регистрация и аутентификация пользователей с использованием JWT токенов.
- Поиск и бронирование билетов на маршрутки.
- Генерация и отображение QR-кода после подтверждения бронирования.
- Просмотр истории бронирования для зарегистрированных пользователей.
- Административная панель для управления городами, маршрутами, билетами и расписанием, включая функционал поиска.

## Административная панель

Администраторы могут:
- Добавлять, удалять и изменять информацию о городах.
- Управлять маршрутами, включая добавление, удаление и редактирование.
- Просматривать все купленные билеты и осуществлять дополнительные действия по управлению ими.
- Выполнять CRUD операции с расписанием, включая добавление новых записей, редактирование существующих и удаление неактуальных.
- Поиск по названию города, маршрута и имени пользователя для удобного поиска и фильтрации данных на административной панели.
  
## Структура базы данных

Ниже приведена структура базы данных:

![Структура базы данных](https://github.com/Rakhmonberdiev/Diplom/blob/master/Baza.png?raw=true)

## Скриншоты
Главная страница:
![scren1](https://github.com/Rakhmonberdiev/Diplom/blob/master/scren1.png?raw=true)
![scren2](https://github.com/Rakhmonberdiev/Diplom/blob/master/scren2.png?raw=true)
![scren3](https://github.com/Rakhmonberdiev/Diplom/blob/master/scren3.png?raw=true)

Админ-панель:
![scren4](https://github.com/Rakhmonberdiev/Diplom/blob/master/scren4.png?raw=true)
![scren5](https://github.com/Rakhmonberdiev/Diplom/blob/master/scren5.png?raw=true)

Регистрация:
![scren6](https://github.com/Rakhmonberdiev/Diplom/blob/master/scren6.png?raw=true)


## Дополнительная информация

[jr-blog.uz](https://jr-blog.uz)
