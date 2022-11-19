namespace WebApp.Models;

public class Alert
{
    public Alert(string message, AlertType alertType = AlertType.Success)
    {
        this.Message = message;

        this.ColorClasses = alertType switch
        {
            AlertType.Success => "bg-green-600 border-green-700",
            AlertType.Information => "bg-blue-600 border-blue-700",
            _ => "bg-red-600 border-red-700"
        };
    }

    public string ColorClasses { get; init; }

    public string Message { get; init; }
}
