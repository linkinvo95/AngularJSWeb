using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using DocumentFormat.OpenXml.Spreadsheet;
using Raven.Client.Indexes;

namespace Data.Indexes
{
   public class OrderListIndex : AbstractIndexCreationTask<Order>
    {
        public OrderListIndex()
        {
            Map = orders => from order in orders
                select new
                {
                    order.Number,
                    UserId = order.User.Id,
                    order.Date
                };
        }
    }
}
