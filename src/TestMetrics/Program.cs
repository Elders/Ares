using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using metrics;
using metrics.Core;

namespace TestMetrics
{
    class Program
    {
        static void Main(string[] args)
        {
            new ThingFinder().FindThings();
        }
    }

    public class ThingFinder
    {
        // Measure the # of records per second returned
        private Metrics _resultsMeter = new Metrics();//.Meter(typeof(ThingFinder), "results", TimeUnit.Seconds)

        // Measure the # of milliseconds each query takes and the number of queries per second being performed
        //private IMetric _dbTimer = Metrics.Timer(typeof(ThingFinder), "database", TimeUnit.Milliseconds, TimeUnit.Seconds)

        public void FindThings()
        {
            //// Perform an action which gets timed
            //var results = _dbTimer.Time(() =>
            //{
            //    Database.Query("SELECT Unicorns FROM Awesome");
            //}

            // Calculate the rate of new things found
            //var asd = _resultsMeter.Gauge("context", "success", () => 5);
            //asd.Counter("failed.SendUser");
            //gay.Counter("failed.Command.SendUser")
            // etc.
        }
    }

    interface ICronusMetricsDraft
    {
        void Gauge(string name, long value, string boundedContext, string application, string tenant, string machine, string cluster);
        void Gauge(string name, long value = 1);

        void Counter(string name, long value, string boundedContext, string application, string tenant, string machine, string cluster);
        void Counter(string name, long value = 1);

        void Timer(string name, long timeElapsedInMilliseconds, string boundedContext, string application, string tenant, string machine, string cluster);
        void Timer(string name, long timeElapsedInMilliseconds);
    }
}
