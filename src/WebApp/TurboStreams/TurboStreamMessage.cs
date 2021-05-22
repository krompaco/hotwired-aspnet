using System;

namespace WebApp.TurboStreams
{
    public class TurboStreamMessage
    {
        public const string MimeType = "text/vnd.turbo-stream.html";

        public string Target { get; set; }

        public TurboStreamAction Action { get; set; }

        /// <summary>
        /// Gets or sets the content inside <template></template>. Not needed if action is Remove.
        /// </summary>
        public string TemplateInnerHtml { get; set; }

        public override string ToString()
        {
            var template = this.Action == TurboStreamAction.Remove ?
                string.Empty
                : $"<template>{this.TemplateInnerHtml}</template>";

            return $"<turbo-stream action=\"{this.GetActionString()}\" target=\"{this.Target}\">{template}</turbo-stream>";
        }

        private string GetActionString()
        {
            return this.Action switch
            {
                TurboStreamAction.Append => "append",
                TurboStreamAction.Prepend => "prepend",
                TurboStreamAction.Replace => "replace",
                TurboStreamAction.Update => "update",
                _ => "remove"
            };
        }
    }
}
