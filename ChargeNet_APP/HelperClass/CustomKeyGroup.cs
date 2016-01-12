using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneApp1.HelperClass;
using PhoneApp1.Models;

namespace PhoneApp1.HelperClass
{
    public class CustomKeyGroup<T> : List<T>
    {

        public static IEnumerable<MovieDetail> GetMovieList(List<MovieDetail> items)
        {

            List<MovieDetail> movieList = new List<MovieDetail>();

            movieList = items;

            return movieList;

        }

        public static List<Group<MovieDetail>> GetMovieGroups(List<MovieDetail> items)
        {

            IEnumerable<MovieDetail> movieList = GetMovieList(items);

            return GetItemGroups(movieList, c => c.Title);

        }

        public static List<Group<T>> GetItemGroups<T>(IEnumerable<T> itemList, Func<T, string> getKeyFunc)
        {

            IEnumerable<Group<T>> groupList = from item in itemList

                                              group item by getKeyFunc(item) into g

                                              orderby g.Key

                                              select new Group<T>(g.Key, g);

            return groupList.ToList();

        }

        public class Group<T> : List<T>
        {

            public Group(string name, IEnumerable<T> items)

                : base(items)
            {

                this.Title = name;

            }

            public string Title
            {

                get;

                set;

            }

        }

    }
}
