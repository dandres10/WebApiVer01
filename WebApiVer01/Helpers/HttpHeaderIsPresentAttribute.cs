namespace WebApiVer01.Helpers
{
    using Microsoft.AspNetCore.Mvc.ActionConstraints;
    using System;

    public class HttpHeaderIsPresentAttribute: Attribute, IActionConstraint
    {
        private string _header;
        private string _value;

        public HttpHeaderIsPresentAttribute(string header, string value)
        {
            _header = header;
            _value = value;
        }

        public int Order => 0;

        public bool Accept(ActionConstraintContext context)
        {
            var headers = context.RouteContext.HttpContext.Request.Headers;

            if (!headers.ContainsKey(_header))
            {
                return false;
            }

            return string.Equals(headers[_header], _value, StringComparison.OrdinalIgnoreCase);
        }
    }
}