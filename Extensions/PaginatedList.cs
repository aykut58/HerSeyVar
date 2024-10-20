using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HerSeyVar.Helpers
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; private set; } // Elemanlar
        public int PageIndex { get; private set; } // Geçerli sayfa
        public int TotalPages { get; private set; } // Toplam sayfa sayısı

        public bool HasPreviousPage => PageIndex > 1; // Önceki sayfa var mı?
        public bool HasNextPage => PageIndex < TotalPages; // Sonraki sayfa var mı?

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize); // Toplam sayfa hesaplama
            Items = items; // Elemanları ata
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync(); // Toplam eleman sayısını al
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(); // Sayfalama uygula
            return new PaginatedList<T>(items, count, pageIndex, pageSize); // Yeni sayfalı liste döndür
        }
    }
}
