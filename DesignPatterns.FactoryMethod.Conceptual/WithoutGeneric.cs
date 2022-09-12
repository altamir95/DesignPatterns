using System;

namespace DesignPatterns.FactoryMethod.Conceptual
{
    // Класс Создатель объявляет фабричный метод, который должен возвращать
    // объект класса Продукт. Подклассы Создателя обычно предоставляют
    // реализацию этого метода.
    abstract class Creator
    {
        // Обратите внимание, что Создатель может также обеспечить реализацию
        // фабричного метода по умолчанию.
        public abstract ILicense FactoryMethod();

        // Также заметьте, что, несмотря на название, основная обязанность
        // Создателя не заключается в создании продуктов. Обычно он содержит
        // некоторую базовую бизнес-логику, которая основана  на объектах
        // Продуктов, возвращаемых фабричным методом.  Подклассы могут косвенно
        // изменять эту бизнес-логику, переопределяя фабричный метод и возвращая
        // из него другой тип продукта.
        public string CreateLicense()
        {
            // Вызываем фабричный метод, чтобы получить объект-продукт.
            var license = FactoryMethod();

            // Далее, работаем с этим продуктом.
            var result = license.Create();

            return result;
        }
    }

    // Конкретные Создатели переопределяют фабричный метод для того, чтобы
    // изменить тип результирующего продукта.
    class CarLicenseCreator : Creator
    {
        // Обратите внимание, что сигнатура метода по-прежнему использует тип
        // абстрактного продукта, хотя  фактически из метода возвращается
        // конкретный продукт. Таким образом, Создатель может оставаться
        // независимым от конкретных классов продуктов.
        public override ILicense FactoryMethod()
        {
            return new CarLicense();
        }
    }

    class GunLicenseCreator : Creator
    {
        public override ILicense FactoryMethod()
        {
            return new GunLicense();
        }
    }

    // Интерфейс Продукта объявляет операции, которые должны выполнять все
    // конкретные продукты.
    public interface ILicense
    {
        string Create();
    }

    // Конкретные Продукты предоставляют различные реализации интерфейса
    // Продукта.
    class CarLicense : ILicense
    {
        public string Create()
        {
            return "{Created car license}";
        }
    }

    class GunLicense : ILicense
    {
        public string Create()
        {
            return "{Created gun license}";
        }
    }

    class Client
    {
        public void Main()
        {
            Console.WriteLine("App: Launched with the ConcreteCreator1.");
            ClientCode(new CarLicenseCreator());

            Console.WriteLine("");

            Console.WriteLine("App: Launched with the ConcreteCreator2.");
            ClientCode(new GunLicenseCreator());
        }

        // Клиентский код работает с экземпляром конкретного создателя, хотя и
        // через его базовый интерфейс. Пока клиент продолжает работать с
        // создателем через базовый интерфейс, вы можете передать ему любой
        // подкласс создателя.
        public void ClientCode(Creator creator)
        {
            // ...
            Console.WriteLine("Client: I'm not aware of the creator's class," +
                "but it still works.\n" + creator.CreateLicense());
            // ...
        }
    }
}