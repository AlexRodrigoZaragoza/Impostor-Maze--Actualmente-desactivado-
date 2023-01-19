
namespace UI
{
    public class ErrorMessages
    {
        public string _errorText = "";

        public void ShowError(string error)
        {
            _errorText = error;
        }
    }
}
