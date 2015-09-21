using Machine.Specifications;

namespace Elders.Ares.Tests
{

    public class When_execute_command_which_does_not_fail
    {
        Because of = () => result = AresExecutor.Execute("success", () => { });

        It should_return__success__response = () => result.IsSuccess.ShouldBeTrue();

        static OperationResult result;
    }
}