using System;

namespace DeveloperSample.ClassRefactoring
{
    public enum SwallowType
    {
        African, European
    }

    public enum SwallowLoad
    {
        None, Coconut
    }

    // public class SwallowFactory
    // {
    //     public Swallow GetSwallow(SwallowType swallowType) => new Swallow(swallowType);
    // }

    public abstract class Swallow{
        public SwallowLoad Load { get; private set; }

        public void ApplyLoad(SwallowLoad load)
        {
            Load = load;
        }

        public abstract double GetAirspeedVelocity();

    }

    public class AfricanSwallow : Swallow
    {
        public override double GetAirspeedVelocity()
        {
            return Load == SwallowLoad.Coconut ? 18 : 22;
        }
    }

    public class EuropeanSwallow : Swallow
    {
        public override double GetAirspeedVelocity()
        {
            return Load == SwallowLoad.Coconut ? 16 : 20;
        }
    }

    public class SwallowFactory
    {
        public Swallow GetSwallow(SwallowType swallowType) => swallowType switch
        {
            SwallowType.African => new AfricanSwallow(),
            SwallowType.European => new EuropeanSwallow(),
            _ => throw new ArgumentException("Invalid swallow type")
        };
    }

    // public class Swallow
    // {
    //     public SwallowType Type { get; }
    //     public SwallowLoad Load { get; private set; }

    //     public Swallow(SwallowType swallowType)
    //     {
    //         Type = swallowType;
    //     }

    //     public void ApplyLoad(SwallowLoad load)
    //     {
    //         Load = load;
    //     }

    //     public double GetAirspeedVelocity()
    //     {
    //         if (Type == SwallowType.African && Load == SwallowLoad.None)
    //         {
    //             return 22;
    //         }
    //         if (Type == SwallowType.African && Load == SwallowLoad.Coconut)
    //         {
    //             return 18;
    //         }
    //         if (Type == SwallowType.European && Load == SwallowLoad.None)
    //         {
    //             return 20;
    //         }
    //         if (Type == SwallowType.European && Load == SwallowLoad.Coconut)
    //         {
    //             return 16;
    //         }
    //         throw new InvalidOperationException();
    //     }
    // }
}