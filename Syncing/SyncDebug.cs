using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeveloperSample.Syncing
{
    
    //InitializeList
    //had to rely heavily on looking up why this method didn't work.
    //I know that it had to do with async vs synchronous and awaiting. 
    //That is why the test was failing with missing inputs.

    //InitializeDictionary
    //had to rely heavily on looking up why this method didn't work.
    //I know that it could be a race condition due to multiple thread access to resources at same time.
    //Having the threads working on non overlapping sections would help. 
    //But a lot of the Linq syntax and methods I didn't remember.
    public class SyncDebug
    {
        public async Task<List<string>> InitializeList(IEnumerable<string> items)
        {
            // var bag = new List<string>();
            // Parallel.ForEach(items, async i =>
            // {
            //     var r = await Task.Run(() => i).ConfigureAwait(false);
            //     bag.Add(r);
            // });

            var bag = new ConcurrentBag<string>();

            var tasks = items.Select(async i =>
            {
                var r = await Task.Run(() => i).ConfigureAwait(false);
                bag.Add(r);
            });

            await Task.WhenAll(tasks);

            var list = bag.ToList();
            return list;
        }

        public Dictionary<int, string> InitializeDictionary(Func<int, string> getItem)
        {
            var itemsToInitialize = Enumerable.Range(0, 100).ToList();

            var concurrentDictionary = new ConcurrentDictionary<int, string>();

            var partitionSize = itemsToInitialize.Count / 2;

            var threads = Enumerable.Range(0, 3)
                .Select(i =>
                {
                    var partition = itemsToInitialize.Skip(i * partitionSize).Take(partitionSize).ToList();
                    return new Thread(() =>
                    {
                        foreach (var item in partition)
                        {
                            concurrentDictionary.AddOrUpdate(item, getItem, (_, s) => s);
                        }
                    });
                })
                .ToList();

                // .Select(i => new Thread(() => {
                //     foreach (var item in itemsToInitialize)
                //     {
                //         concurrentDictionary.AddOrUpdate(item, getItem, (_, s) => s);
                //     }
                // }))
                // .ToList();

            foreach (var thread in threads)
            {
                thread.Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }

            return concurrentDictionary.ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}