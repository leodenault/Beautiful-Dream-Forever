using NUnit.Framework;

[TestFixture]
public class WardrobePaginatorTest {

	private int[] createData(int size) {
		int[] data = new int[size];
		for (int i = 1; i <= size; i++) {
			data[i - 1] = i;
		}
		return data;
	}

	[Test]
	public void PaginatorReturnsSamePageIfAtEndOfData() {
		WardrobePaginator<int> paginator = new WardrobePaginator<int>(createData(16));
		int[] page1 = paginator.Current();
		int[] page2 = paginator.Next();
		int[] page3 = paginator.Next();
		Assert.AreNotEqual(page1, page2);
		Assert.AreEqual(page2, page3);
	}

	[Test]
	public void PaginatorHas15AsDefaultPageSize() {
		WardrobePaginator<int> paginator15 = new WardrobePaginator<int>(createData(15));
		WardrobePaginator<int> paginator16 = new WardrobePaginator<int>(createData(16));

		Assert.AreEqual(15, paginator15.Current().Length);
		Assert.AreEqual(15, paginator16.Current().Length);
	}

	[Test]
	public void PreviousReturnsSamePageIfAtBeginningOfData() {
		WardrobePaginator<int> paginator = new WardrobePaginator<int>(createData(16));
		paginator.Next();
		int[] page = paginator.Previous();
		Assert.AreEqual(page, paginator.Previous());
	}

	[Test]
	public void CurrentReturnsCurrentPage() {
		WardrobePaginator<int> paginator = new WardrobePaginator<int>(createData(33));
		Assert.AreEqual(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}, paginator.Current());

		int[] page = paginator.Next();
		Assert.AreEqual(page, paginator.Current());

		page = paginator.Next();
		Assert.AreEqual(page, paginator.Current());
	}

	[Test]
	public void NextReturnsNextSetOfData() {
		WardrobePaginator<int> paginator = new WardrobePaginator<int>(createData(10), 3);
		Assert.AreEqual(new int [] {1, 2, 3}, paginator.Current());
		Assert.AreEqual(new int [] {4, 5, 6}, paginator.Next());
		Assert.AreEqual(new int [] {7, 8, 9}, paginator.Next());
		Assert.AreEqual(new int[] { 10 }, paginator.Next());
	}

	[Test]
	public void PreviousReturnsPreviousSetOfData() {
		WardrobePaginator<int> paginator = new WardrobePaginator<int>(createData(10), 3);
		paginator.Next();
		paginator.Next();
		Assert.AreEqual(new int [] {10}, paginator.Next());
		Assert.AreEqual(new int [] {7, 8, 9}, paginator.Previous());
		Assert.AreEqual(new int [] {4, 5, 6}, paginator.Previous());
		Assert.AreEqual(new int[] { 1, 2, 3 }, paginator.Previous());
	}
}
