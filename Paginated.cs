public class Paginated<T> : List<T>
	{
		public int Page { get; }
		public int Pages { get; }
		public int Total { get; }

		private Paginated(List<T> items, int total, int page, int quantity)
		{
			Page = page;
			Pages = (int) Math.Ceiling(total / (double) quantity);
			Total = total;
			AddRange(items);
		}

		public static Paginated<T> Create(IEnumerable<T> source, int page, int quantity)
		{
			page = page == 0 ? 1 : page;
			quantity = quantity == 0 ? 1 : quantity;

			int count = source.Count();

			List<T>? list = source
				.Skip((page - 1) * quantity)
				.Take(quantity)
				.ToList();

			return new Paginated<T>(list, count, page, quantity);
		}
	}
