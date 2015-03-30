using System;
using System.Collections.Generic;

public class WardrobePaginator<T> {
	private const int DEFAULT_PAGE_SIZE = 15;

	private int index;
	private IList<T[]> sets;

	public WardrobePaginator(T[] data) {
		init(data, DEFAULT_PAGE_SIZE);
	}

	public WardrobePaginator(T[] data, int pageSize) {
		init(data, pageSize);
	}
	
	private void init(T[] data, int pageSize) {
		sets = new List<T[]>();
		index = 0;

		for (int i = 0; i < data.Length; i += pageSize) {
			int setSize = Math.Min(pageSize, data.Length - i);
			T[] currentSet = new T[setSize];
			Array.Copy(data, i, currentSet, 0, setSize);
			sets.Add(currentSet);
		}
	}


	public T[] Next() {
		index = (index == sets.Count - 1) ? index : index + 1;
		return sets[index];
	}

	public T[] Previous() {
		index = (index > 0) ? index - 1 : index;
		return sets[index];
	}

	public T[] Current() {
		return sets[index];
	}
}
