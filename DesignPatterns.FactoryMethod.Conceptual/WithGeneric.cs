using System;

namespace DesignPatterns.FactoryMethod.Conceptual.Generic
{
    public interface ICreator
    {
        public string CreateLicense();
    }

    public class Creator<FactoryT>: ICreator where FactoryT : ILicense 
    {
        public FactoryT FactoryObject { get; set; }

        public Creator(FactoryT factoryObject)
        {
            FactoryObject = factoryObject;
        }

        public string CreateLicense()
        {
            return FactoryObject.Create();
        }
    }

    public interface ILicense
    {
        string Create();
    }

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
            ClientCode(new Creator<CarLicense>(new CarLicense()));

            ClientCode(new Creator<GunLicense>(new GunLicense()));
        }

        public void ClientCode(ICreator creator)
        {
            Console.WriteLine(creator.CreateLicense());
        }
    }
}

