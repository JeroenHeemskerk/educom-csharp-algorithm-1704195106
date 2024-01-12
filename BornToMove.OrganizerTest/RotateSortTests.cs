using NUnit.Framework;
using Org;

namespace BornToMove.OrganizerTest
{
    public class RotateSortTests
    {
        public RotateSort<int> sort = new RotateSort<int>();

        public IComparer<int> comp = Comparer<int>.Default;

        public void testSortEmpty()
        {
            //prepare

            List<int> array = new List<int>();

            //run

            List<int> sortedArray = sort.Sort(array, comp);

            //validate

            array.Sort();

            Assert.That(array, Is.Not.Null);
            Assert.That(array, Has.Exactly(0).Items);
            Assert.That(array, Is.EquivalentTo(new int[] {}));
            // also check that our input is not modified
            Assert.That(array, Is.EquivalentTo(new int[] {}));
        }

        public void testSortOneElement()
        {
            //prepare

            List<int> array = new List<int>() { 1 };

            //run

            List<int> sortedArray = sort.Sort(array, comp);

            //validate

            Assert.That(array, Is.Not.Null);
            Assert.That(array, Has.Exactly(1).Items);
            Assert.That(array, Is.EquivalentTo(new int[] { 1 }));
            // also check that our input is not modified
            Assert.That(array, Is.EquivalentTo(new int[] { 1 }));
        }

        public void testSortTwoElements()
        {
            //prepare

            List<int> array = new List<int>() { 2, 1 };

            //run

            List<int> sortedArray = sort.Sort(array, comp);

            //validate

            Assert.That(array, Is.Not.Null);
            Assert.That(array, Has.Exactly(2).Items);
            Assert.That(array, Is.EquivalentTo(new int[] { 1, 2 }));
            // also check that our input is not modified
            Assert.That(array, Is.EquivalentTo(new int[] { 2, 1 }));
        }

        public void testSortThreeEqual()
        {
            //prepare

            List<int> array = new List<int>() { 3, 3, 3 };

            //run

            List<int> sortedArray = sort.Sort(array, comp);

            //validate

            Assert.That(array, Is.Not.Null);
            Assert.That(array, Has.Exactly(3).Items);
            Assert.That(array, Is.EquivalentTo(new int[] { 3, 3, 3 }));
            // also check that our input is not modified
            Assert.That(array, Is.EquivalentTo(new int[] { 3, 3, 3 }));
        }

        public void testSortUnsortedArray()
        {
            //prepare

            List<int> array = new List<int>() { 2, 1, 3 };

            //run

            List<int> sortedArray = sort.Sort(array, comp);

            //validate

            Assert.That(array, Is.Not.Null);
            Assert.That(array, Has.Exactly(3).Items);
            Assert.That(array, Is.EquivalentTo(new int[] { 1, 2, 3 }));
            // also check that our input is not modified
            Assert.That(array, Is.EquivalentTo(new int[] { 2, 1, 3 }));
        }

        public void testSortUnsortedThreeEqual()
        {
            //prepare

            List<int> array = new List<int>() { 1, 3, 6, 3, 2, 4, 3 };

            //run

            List<int> sortedArray = sort.Sort(array, comp);

            //validate

            Assert.That(array, Is.Not.Null);
            Assert.That(array, Has.Exactly(7).Items);
            Assert.That(array, Is.EquivalentTo(new int[] { 1, 2, 3, 3, 3, 4, 6 }));
            // also check that our input is not modified
            Assert.That(array, Is.EquivalentTo(new int[] { 1, 3, 6, 3, 2, 4, 3 }));
        }
    }
}
