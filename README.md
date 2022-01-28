# Информационная система «Molotok34»

Для данной ИС было разработано:
- [Web-app ASP.NET MVC](https://github.com/3uph0ria/molotok34#web-app-aspnet-mvc)
- [Web-API](https://github.com/3uph0ria/molotok34#web-api) 
- [WCF Service](https://github.com/3uph0ria/molotok34#wcf-service)
- [Desktop app (WPF)](https://github.com/3uph0ria/molotok34#wpf-app)
 
## Web-app ASP.NET MVC
- [Главная страница](http://gsmolotok34-001-site1.htempurl.com)
- [Админка](http://gsmolotok34-001-site1.htempurl.com/Admin/SignIn) Данные для входа: tester:1234 (log:pass).

Клиент на сайте может, смотреть каталог, делать сортировку и поиск товаров, совершать покупки (реальной оплаты на сайте нету!), смотреть контактную информацию.

Администратор, после авторизации в панели управления получает роль с определенным набором прав для доступа к внесению изменений в БД.

### Главная страница

![Превью](https://github.com/3uph0ria/molotok34/blob/master/img/1.png?raw=true)

### Каталог
Поиск и фильтрация товаров на JS.

![Превью](https://github.com/3uph0ria/molotok34/blob/master/img/2.png?raw=true)

### Страница товара
![Превью](https://github.com/3uph0ria/molotok34/blob/master/img/3.png?raw=true)

### Страница контактов
![Превью](https://github.com/3uph0ria/molotok34/blob/master/img/4.png?raw=true)

### Панель администратора
![Превью](https://github.com/3uph0ria/molotok34/blob/master/img/5.jpg?raw=true)

### Страницы добавления/редактирвоания/уадления
![Превью](https://github.com/3uph0ria/molotok34/blob/master/img/6.jpg?raw=true)

## Web-API
[Документация](http://gsportfolio-001-site1.btempurl.com/Help)

![Превью](https://github.com/3uph0ria/molotok34/blob/master/img/7.jpg?raw=true)

## WCF Service
В нем разработан класс для работы с ранее созданным Web-API, для работы запустите APiHost (molotok34\wcf\Molotok34\ApiHost\bin\Debug.exe)

![Превью](https://github.com/3uph0ria/molotok34/blob/master/img/8.jpg?raw=true)

## WPF-App
Перед запуском, необходимо запустить WCF-host (molotok34\wcf\Molotok34\ApiHost\bin\Debug.exe)

Администратор, после авторизации получает роль с определенным набором прав для доступа к внесению изминений информации в БД через API.

### Авторизация
![Превью](https://github.com/3uph0ria/molotok34/blob/master/img/9.jpg?raw=true)

### Пример страниц вывода строк
![Превью](https://github.com/3uph0ria/molotok34/blob/master/img/10.jpg?raw=true)

### Пример страниц добавления/редактирования
![Превью](https://github.com/3uph0ria/molotok34/blob/master/img/11.jpg?raw=true)
