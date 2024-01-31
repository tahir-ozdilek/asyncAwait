namespace Delegates
{
    public class Program
    {
        public delegate int IntDelegate(int a, int b);

        public delegate void VoidDelegate(int a, int b);
       
        public delegate bool BoolDelegate(int a, int b);

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
            IntDelegate intDelegate = delegateFunction;
            intDelegate(1, 2);
            
            actionDelegate(2, 2); //actionDelegate.Invoke(2, 2);
            funcDelegate(2, 3); //funcDelegate.Invoke(2, 3);

            //A delegate referencing to a anonymous method
            VoidDelegate voidDelegate = (a, b) => { Console.WriteLine("inline anonymous:" + (a + b) ); };
            voidDelegate(3, 3);

            //A delegate referencing to an expression
            BoolDelegate boolDelegate = (a, b) => a == b;

            Console.WriteLine(boolDelegate.Invoke(9,8));
            Console.WriteLine(boolDelegate.Invoke(9,9));

            methodTakesFuncParameter(actionFunction);

            methodTakesFuncParameter((a,b) => { Console.WriteLine("inline anonymous method is being called: "+ (a+b)); });
        }


        static void methodTakesFuncParameter(Action<int, int> action)
        {
            Console.WriteLine("action is being called by action parameter.");
            action.Invoke(4, 3);
        }
    }
}
