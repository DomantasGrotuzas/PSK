using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using PSK.Domain.Enums;

namespace PSK.FrontEnd.Static
{
    public static class ViewTexts
    {
        public static readonly List<SelectListItem> PurchasableStatusItems = new List<SelectListItem>
        {
            new SelectListItem("Not needed", PurchasableStatus.NotNeeded.ToString()),
            new SelectListItem("Requested", PurchasableStatus.Requested.ToString()),
            new SelectListItem("Purchased/Booked", PurchasableStatus.Purchased.ToString())
        };
    }
}