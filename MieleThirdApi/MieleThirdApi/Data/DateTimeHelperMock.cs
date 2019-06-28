using System;
using System.Collections.Generic;
using System.Text;

namespace MieleThirdApi.Data
{
    public class DateTimeHelperMock : IDateTimeHelper
    {
        public DateTime DateTimeMock { get; set; }

        public DateTime Now()
        {
            return DateTimeMock;
        }
    }
}
