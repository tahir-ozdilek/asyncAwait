namespace Delegates
{
    public class Program
    {
        public delegate int MyDelegate(int a, int b);

        public static int delegateFunction(int a, int b)
        {
            Console.WriteLine("delegateFunction " + (a + b));
            return a + b;
        }

        public static Action<int, int> actionDelegate = actionFunction;
        public static Func<int, int, int> funcDelegate = funcFunction;

        static int funcFunction(int a, int b)
        {
            Console.WriteLine("funcFunction " + (a + b));
            return a + b;
        }

        static void actionFunction(int a, int b)
        {
            Console.WriteLine("actionFunction: " + (a + b));
        }

        static void Main(string[] args)
        {
            MyDelegate myDelegate = delegateFunction;
            myDelegate(4, 5);

            actionDelegate(1, 2); //actionDelegate.Invoke(1, 2);
            funcFunction(2, 3);


            methodTakesFuncParameter(actionFunction);
        }


        static void methodTakesFuncParameter(Action<int, int> action)
        {
            Console.WriteLine("action is being called by action parameter.");
            action.Invoke(4, 5);
        }
    }
}
