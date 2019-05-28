using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using PSK.Domain.Enums;

namespace PSK.FrontEnd.Static
{
    public static class ViewTexts
    {
        public static readonly List<SelectListItem> PurchasableStatusItems = new List<SelectListItem>
        {
            new SelectListItem(PurchasableStatus.NotNeeded.ToString(), PurchasableStatus.NotNeeded.ToString()),
            new SelectListItem(PurchasableStatus.Requested.ToString(), PurchasableStatus.Requested.ToString()),
            new SelectListItem(PurchasableStatus.Purchased.ToString(), PurchasableStatus.Purchased.ToString())
        };
    }
}