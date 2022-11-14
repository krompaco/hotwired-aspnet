using WebApp.Models;

namespace WebApp.Pages.Shared.Partials.Interaction;

/// <summary>
/// Shows a notification/alert. Returns a string so can be used from pretty much anywhere.
/// </summary>
public static class Alert
{
    public static string GetHtml(string message, AlertType alertType = AlertType.Success)
    {
        var colorClasses = alertType switch
        {
            AlertType.Success => "bg-green-600 border-green-700",
            AlertType.Information => "bg-blue-600 border-blue-700",
            _ => "bg-red-600 border-red-700"
        };

        return $"""
                <div class="fixed inset-x-0 top-0 flex items-end justify-right px-4 py-6 sm:p-6 justify-end z-30 pointer-events-none">
                   <div data-controller="alert" data-alert-dismiss-after-value="2500" data-alert-show-class="translate-x-0 opacity-100" data-alert-hide-class="translate-x-full opacity-0" class="max-w-sm w-full shadow-lg px-4 py-3 relative {colorClasses} border-l-4 text-white pointer-events-auto transition translate-x-full transform ease-in-out duration-1000 opacity-0">
                        <div class="p-2">
                            <div class="flex items-center">
                                <div class="ml-3 w-0 flex-1 pt-0.5">
                                    <p class="text-xs sm:text-sm leading-5 font-medium">{message}</p>
                                </div>
                                <div class="ml-4 flex-shrink-0 flex">
                                    <button data-action="alert#close" class="inline-flex text-white focus:outline-none focus:text-gray-300">
                                        <svg class="h-5 w-5" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><line x1="18" y1="6" x2="6" y2="18"></line><line x1="6" y1="6" x2="18" y2="18"></line></svg>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
          """;
    }
}
