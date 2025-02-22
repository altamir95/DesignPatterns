﻿// https://refactoring.guru/ru/design-patterns/visitor
// https://refactoring.guru/ru/design-patterns/visitor/csharp/example

using System;
using System.Collections.Generic;

namespace DesignPatterns.Visitor.Conceptual
{
    // Интерфейс Компонента объявляет метод accept, который в качестве аргумента
    // может получать любой объект, реализующий интерфейс посетителя.
    public interface IComponent
    {
        void Accept(IVisitor visitor);
    }

    // Каждый Конкретный Компонент должен реализовать метод Accept таким
    // образом, чтобы он вызывал метод посетителя, соответствующий классу
    // компонента.
    public class Component1 : IComponent
    {
        // Обратите внимание, мы вызываем VisitConcreteComponentA, что
        // соответствует названию текущего класса. Таким образом мы позволяем
        // посетителю узнать, с каким классом компонента он работает.
        public void Accept(IVisitor v)
        {
            v.Visit(this);
        }

        // Конкретные Компоненты могут иметь особые методы, не объявленные в их
        // базовом классе или интерфейсе. Посетитель всё же может использовать
        // эти методы, поскольку он знает о конкретном классе компонента.
        public string ExclusiveMethodOfConcreteComponentA()
        {
            return "A";
        }
    }

    public class Component2 : IComponent
    {
        // То же самое здесь: VisitConcreteComponentB => ConcreteComponentB
        public void Accept(IVisitor v)
        {
            v.Visit(this);
        }

        public string SpecialMethodOfConcreteComponentB()
        {
            return "B";
        }
    }

    // Интерфейс Посетителя объявляет набор методов посещения, соответствующих
    // классам компонентов. Сигнатура метода посещения позволяет посетителю
    // определить конкретный класс компонента, с которым он имеет дело.
    public interface IVisitor
    {
        void Visit(Component1 element);

        void Visit(Component2 element);
    }

    // Конкретные Посетители реализуют несколько версий одного и того же
    // алгоритма, которые могут работать со всеми классами конкретных
    // компонентов.
    //
    // Максимальную выгоду от паттерна Посетитель вы почувствуете, используя его
    // со сложной структурой объектов, такой как дерево Компоновщика. В этом
    // случае было бы полезно хранить некоторое промежуточное состояние
    // алгоритма при выполнении методов посетителя над различными объектами
    // структуры.
    class ConcreteVisitor1 : IVisitor
    {
        public void Visit(Component1 element)
        {
            Console.WriteLine(element.ExclusiveMethodOfConcreteComponentA() + " + ConcreteVisitor1");
        }

        public void Visit(Component2 element)
        {
            Console.WriteLine(element.SpecialMethodOfConcreteComponentB() + " + ConcreteVisitor1");
        }
    }

    class ConcreteVisitor2 : IVisitor
    {
        public void Visit(Component1 element)
        {
            Console.WriteLine(element.ExclusiveMethodOfConcreteComponentA() + " + ConcreteVisitor2");
        }

        public void Visit(Component2 element)
        {
            Console.WriteLine(element.SpecialMethodOfConcreteComponentB() + " + ConcreteVisitor2");
        }
    }

    public class Client
    {
        // Клиентский код может выполнять операции посетителя над любым набором
        // элементов, не выясняя их конкретных классов. Операция принятия
        // направляет вызов к соответствующей операции в объекте посетителя.
        public static void ClientCode(List<IComponent> components, IVisitor visitor)
        {
            foreach (var component in components)
            {
                component.Accept(visitor);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<IComponent> components = new List<IComponent>
            {
                new Component1(),
                new Component2()
            };

            Console.WriteLine("The client code works with all visitors via the base Visitor interface:");
            var visitor1 = new ConcreteVisitor1();
            Client.ClientCode(components, visitor1);

            Console.WriteLine();

            Console.WriteLine("It allows the same client code to work with different types of visitors:");
            var visitor2 = new ConcreteVisitor2();
            Client.ClientCode(components, visitor2);
        }
    }
}