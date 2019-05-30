using System;
using System.Collections.Generic;
using Contracts;
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

        public static readonly List<SelectListItem> TripSortTypes = new List<SelectListItem>
        {
            new SelectListItem(TripSortType.Cheapest.ToString(), Convert.ToInt32(TripSortType.Cheapest).ToString()),
            new SelectListItem(TripSortType.MostExpensive.ToString(), Convert.ToInt32(TripSortType.MostExpensive).ToString()),
            new SelectListItem(TripSortType.Shortest.ToString(), Convert.ToInt32(TripSortType.Shortest).ToString()),
            new SelectListItem(TripSortType.Longest.ToString(), Convert.ToInt32(TripSortType.Longest).ToString())
        };
    }
}