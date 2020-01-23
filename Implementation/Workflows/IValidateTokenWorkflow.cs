using Implementation.Entities;

namespace Implementation.Workflows
{
    public interface IValidateTokenWorkflow
    {
        User Validate(string token, string username);
    }
}
