using System;
using Machine.Specifications;

namespace Elders.Ares.Tests
{
    public class When_execute_command_which_does_fail
    {
        Because of = () =>
            {
                while (true)
                {
                    result = AresExecutor.Execute("op", () => { throw new Exception("failed"); });
                }

                //result = AresExecutor.Execute("op", () => Console.WriteLine("Operation2"));
            };

        It should_return__fail__response = () => result.IsSuccess.ShouldBeTrue();

        static CircuitBreaker cb;
        static IOperation cmd;
        static OperationResult result;
    }
}