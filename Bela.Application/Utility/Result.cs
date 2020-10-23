using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bela.Application.Utility
{
    public class Result
    {
        public Result(bool isSucessfull, IEnumerable<string> errors, object[] values = null)
        {
            IsSucessfull = isSucessfull;
            Errors = errors.ToArray();
            Values = values;
        }
        public bool IsSucessfull { get; set; }
        public string[] Errors { get; set; }
        public object[] Values { get; set; }

        public static Result Success()
        {
            return new Result(true, new string[] { });
        }

        public static Result Fail(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }

        public static Result Fail()
        {
            return new Result(false, new string[] { });
        }
    }
}
