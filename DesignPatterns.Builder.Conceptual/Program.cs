using System;
using System.Collections.Generic;

namespace DesignPatterns.Builder.Conceptual
{
    // Интерфейс Строителя объявляет создающие методы для различных частей
    // объектов Продуктов.
    public interface IBuilder
    {
        void InstallDoor();

        void InstallWindow();

        void MakeRoof();

        void BuildGarage();

        void BuildSauna();
    }

    // Классы Конкретного Строителя следуют интерфейсу Строителя и предоставляют
    // конкретные реализации шагов построения. Ваша программа может иметь
    // несколько вариантов Строителей, реализованных по-разному.
    public class ConcreteBuilder : IBuilder
    {
        private Building _product = new Building();

        // Новый экземпляр строителя должен содержать пустой объект продукта,
        // который используется в дальнейшей сборке.
        public ConcreteBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            this._product = new Building();
        }

        // Все этапы производства работают с одним и тем же экземпляром
        // продукта.
        public void InstallDoor()
        {
            this._product.Add("Door");
        }

        public void MakeRoof()
        {
            this._product.Add("Roof");
        }

        public void BuildGarage()
        {
            this._product.Add("Garage");
        }

        public void InstallWindow()
        {
            this._product.Add("Window");
        }

        public void BuildSauna()
        {
            this._product.Add("Sauna");
        }

        // Конкретные Строители должны предоставить свои собственные методы
        // получения результатов. Это связано с тем, что различные типы
        // строителей могут создавать совершенно разные продукты с разными
        // интерфейсами. Поэтому такие методы не могут быть объявлены в базовом
        // интерфейсе Строителя (по крайней мере, в статически типизированном
        // языке программирования).
        //
        // Как правило, после возвращения конечного результата клиенту,
        // экземпляр строителя должен быть готов к началу производства
        // следующего продукта. Поэтому обычной практикой является вызов метода
        // сброса в конце тела метода GetProduct. Однако такое поведение не
        // является обязательным, вы можете заставить своих строителей ждать
        // явного запроса на сброс из кода клиента, прежде чем избавиться от
        // предыдущего результата.
        public Building GetBuilding()
        {
            Building result = this._product;

            this.Reset();

            return result;
        }

    }

    // Имеет смысл использовать паттерн Строитель только тогда, когда ваши
    // продукты достаточно сложны и требуют обширной конфигурации.
    //
    // В отличие от других порождающих паттернов, различные конкретные строители
    // могут производить несвязанные продукты. Другими словами, результаты
    // различных строителей  могут не всегда следовать одному и тому же
    // интерфейсу.
    public class Building
    {
        private List<object> _parts = new List<object>();

        public void Add(string part)
        {
            this._parts.Add(part);
        }

        public string ListParts()
        {
            string str = string.Empty;

            for (int i = 0; i < this._parts.Count; i++)
            {
                str += this._parts[i] + ", ";
            }

            str = str.Remove(str.Length - 2); // removing last ",c"

            return "Building parts: " + str + "\n";
        }
    }

    // Директор отвечает только за выполнение шагов построения в определённой
    // последовательности. Это полезно при производстве продуктов в определённом
    // порядке или особой конфигурации. Строго говоря, класс Директор
    // необязателен, так как клиент может напрямую управлять строителями.
    public class Director
    {
        private IBuilder _builder;

        public IBuilder Builder
        {
            set { _builder = value; }
        }

        // Директор может строить несколько вариаций продукта, используя
        // одинаковые шаги построения.
        public void BuildMinimal()
        {
            this._builder.InstallDoor();
        }

        public void BuildFull()
        {
            this._builder.InstallDoor();
            this._builder.MakeRoof();
            this._builder.BuildGarage();
            this._builder.InstallWindow();
            this._builder.InstallWindow();
            this._builder.InstallWindow();
            this._builder.BuildSauna();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Клиентский код создаёт объект-строитель, передаёт его директору,
            // а затем инициирует  процесс построения. Конечный результат
            // извлекается из объекта-строителя.
            var director = new Director();
            var builder = new ConcreteBuilder();
            director.Builder = builder;

            Console.WriteLine("Standard basic product:");
            director.BuildMinimal();
            Console.WriteLine(builder.GetBuilding().ListParts());

            Console.WriteLine("Standard full featured product:");
            director.BuildFull();
            Console.WriteLine(builder.GetBuilding().ListParts());

            // Помните, что паттерн Строитель можно использовать без класса
            // Директор.
            Console.WriteLine("Custom product:");
            builder.InstallDoor();
            builder.BuildGarage();
            Console.Write(builder.GetBuilding().ListParts());
        }
    }
}