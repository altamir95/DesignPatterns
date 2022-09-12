using System;

namespace DesignPatterns.AbstractFactory.Conceptual
{
    // Интерфейс Абстрактной Фабрики объявляет набор методов, которые возвращают
    // различные абстрактные продукты.  Эти продукты называются семейством и
    // связаны темой или концепцией высокого уровня. Продукты одного семейства
    // обычно могут взаимодействовать между собой. Семейство продуктов может
    // иметь несколько вариаций,  но продукты одной вариации несовместимы с
    // продуктами другой.
    public interface IAbstractFactory
    {
        IAbstractGun CreateForGun();

        IAbstractCar CreateForCar();
    }

    // Конкретная Фабрика производит семейство продуктов одной вариации. Фабрика
    // гарантирует совместимость полученных продуктов.  Обратите внимание, что
    // сигнатуры методов Конкретной Фабрики возвращают абстрактный продукт, в то
    // время как внутри метода создается экземпляр  конкретного продукта.
    class ConcreteFactoryLicense : IAbstractFactory
    {
        public IAbstractGun CreateForGun()
        {
            return new ConcreteGunPistol();
        }

        public IAbstractCar CreateForCar()
        {
            return new ConcreteJeep();
        }
    }

    // Каждая Конкретная Фабрика имеет соответствующую вариацию продукта.
    class ConcreteFactoryDocument : IAbstractFactory
    {
        public IAbstractGun CreateForGun()
        {
            return new ConcreteGunRifle();
        }

        public IAbstractCar CreateForCar()
        {
            return new ConcreteSedan();
        }
    }

    // Каждый отдельный продукт семейства продуктов должен иметь базовый
    // интерфейс. Все вариации продукта должны реализовывать этот интерфейс.
    public interface IAbstractGun
    {
        string UsefulFunctionA();
    }

    // Конкретные продукты создаются соответствующими Конкретными Фабриками.
    class ConcreteGunPistol : IAbstractGun
    {
        public string UsefulFunctionA()
        {
            return "The result of the product A1.";
        }
    }

    class ConcreteGunRifle : IAbstractGun
    {
        public string UsefulFunctionA()
        {
            return "The result of the product A2.";
        }
    }

    // Базовый интерфейс другого продукта. Все продукты могут взаимодействовать
    // друг с другом, но правильное взаимодействие возможно только между
    // продуктами одной и той же конкретной вариации.
    public interface IAbstractCar
    {
        // Продукт B способен работать самостоятельно...
        string UsefulFunctionB();

        // ...а также взаимодействовать с Продуктами А той же вариации.
        //
        // Абстрактная Фабрика гарантирует, что все продукты, которые она
        // создает, имеют одинаковую вариацию и, следовательно, совместимы.
        string AnotherUsefulFunctionB(IAbstractGun collaborator);
    }

    // Конкретные Продукты создаются соответствующими Конкретными Фабриками.
    class ConcreteJeep : IAbstractCar
    {
        public string UsefulFunctionB()
        {
            return "The result of the product B1.";
        }

        // Продукт B1 может корректно работать только с Продуктом A1. Тем не
        // менее, он принимает любой экземпляр Абстрактного Продукта А в
        // качестве аргумента.
        public string AnotherUsefulFunctionB(IAbstractGun collaborator)
        {
            var result = collaborator.UsefulFunctionA();

            return $"The result of the B1 collaborating with the ({result})";
        }
    }

    class ConcreteSedan : IAbstractCar
    {
        public string UsefulFunctionB()
        {
            return "The result of the product B2.";
        }

        // Продукт B2 может корректно работать только с Продуктом A2. Тем не
        // менее, он принимает любой экземпляр Абстрактного Продукта А в качестве
        // аргумента.
        public string AnotherUsefulFunctionB(IAbstractGun collaborator)
        {
            var result = collaborator.UsefulFunctionA();

            return $"The result of the B2 collaborating with the ({result})";
        }
    }

    // Клиентский код работает с фабриками и продуктами только через абстрактные
    // типы: Абстрактная Фабрика и Абстрактный Продукт. Это позволяет передавать
    // любой подкласс фабрики или продукта клиентскому коду, не нарушая его.
    class Client
    {
        public void Main()
        {
            // Клиентский код может работать с любым конкретным классом фабрики.
            Console.WriteLine("Client: Testing client code with the first factory type...");
            ClientMethod(new ConcreteFactoryLicense());
            Console.WriteLine();

            Console.WriteLine("Client: Testing the same client code with the second factory type...");
            ClientMethod(new ConcreteFactoryDocument());
        }

        public void ClientMethod(IAbstractFactory factory)
        {
            var productA = factory.CreateForGun();
            var productB = factory.CreateForCar();

            Console.WriteLine(productB.UsefulFunctionB());
            Console.WriteLine(productB.AnotherUsefulFunctionB(productA));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            new Client().Main();
        }
    }
}