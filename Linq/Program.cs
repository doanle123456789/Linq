using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var brands = new List<Brand>()
            {
                new Brand{ID=1, Name="Cong ty AAA"},
                new Brand{ID=2, Name="Cong ty BBB"},
                new Brand{ID=3, Name="Cong ty CCC"}
            };

            var products = new List<Product>()
            {
                new Product(1,"Ban tra", 400, new string[]{"xam","xanh"}, 2),
                new Product(2,"Tranh treo", 400, new string[]{"vang","xanh"}, 1),
                new Product(3,"Den chum", 500, new string[]{"trang"}, 3),
                new Product(4,"Ban hoc", 200, new string[]{"trang","xanh"}, 1),
                new Product(5,"Tui da", 300, new string[]{"do","den","vang"}, 2),
                new Product(6,"Giuong ngu", 500, new string[]{"trang"}, 2),
                new Product(7,"Tu ao", 600, new string[]{"trang"}, 3),
            };

            //lay sp gia 400
            //day la cau truy van linq thuc hien tren nguon products. KQ cua cau truy van nay tra ve tap hop luu trong bien query 
            //bien nay co kieu IEnumerable<Product>, tuc la duyet qua duoc 

            //ve ban chat .net se phantich cau lenh linq, sau do no goi pthuc, nhung API tuong ung cua nguon du lieu, tuc la cua 
            //doi tuong product, sau do tra ve kq va luu vao query. Do do truoc khi tim hieu ve cu phap de viet ra nhung truy van linq
            // chung ta tim hieu nhung phthuc mo rong, nhung API tren nhung doi tuong nhu product, tuc la nhung doi tuong trien khai 
            //tu IEnumerable. Khi we nap thu vien System.Linq thi no se mo rong cho IEnumerable tuc la nhung Array, List, Stack, Queue
            //... co them nhung phuong thuc. Nhung phuong thuc nay de pvu loc, truy van, lay, bien doi du lieu tu mot nguon

            //* Select
           
            //Trong doi tuong products (List<Product>) hay noi cach khac la IEnumerable co 1 phthuc la Select(), Phuong thuc nay
            //nhan mot tham so la mot delegate. delegate day co tham so la kieu cua phan tu va no tra ve mot kieu dlieu gi do
            //Phthuc nay hoat dong la no cho tung phan tu di qua delegate, delegate day tra ve nhung gia tri, nhung gia tri day
            //no se luu vao thanh 1 cai tap hop va tra ve cai tap hop do, tai tham so cua select ta chi viec khai bao nhung cai delegate
            // delegate day co tham so la nhung phan tu cua danh sach, ky hieu la p va delegate day phai tra ve 1 gia tri nao do
            //nhung gia tri do se luu lai thanh 1 cai tap hop.
            //vd: we lay ds ten sp. Voi moi sp p thuoc products chung ta tra ve mot chuoi la ten sp. Kq thi hanh cua phuong thuc nay
            //we luu vao mot bien kq, bien kq nay la mot tap hop IEnumerable, nhung phan tu cua tap hop la chuoi. we can duyet qua
            //nhung phan tu cua kq va in no ra.
            //neu chung ta lay ra Price, thi kq cua no la 1 tap hop cac phan tu kieu double
            //hoac tra ra du lieu kieu vo danh

            var kq = products.Select(
                (p) =>
                {
                    //return p.Name;
                    //return p.Price;
                    return new
                    {
                        Ten = p.Name,
                        Gia = p.Price
                    };
                }
            );

            foreach (var item in kq)
            {
                Console.WriteLine(item);
            }

            //*Where
            //Phuong thuc Where() nhan tham so la 1 delegate, delegate co 1 tham so la kieu cua phan tu va no tra ve kieu logic bool
            //neu no tra ve true thi phan tu day duoc lay ra
            //vd: lay ra nhung san pham ten co chu tr. we viet 1 delegate co 1 tham so la phan tu, tra ve true thi phan tu do duoc
            //lay ra, tra ve false thi phan tu ay khong duoc lay ra, we viet tra ve true neu phan tu ay chua chu tr

            var kq1 = products.Where(
                    (p) =>
                    {
                        //return p.Name.Contains("tr");
                        //return p.Brand == 2;
                        return p.Price >= 200 && p.Price <= 300;
                    }
                );

            foreach (var item in kq1)
            {
                Console.WriteLine(item);
            }

            //*SelectMany
            //Neu dung Select ma return p.Colors thi bien kq se la tap hop mang chuoi. chung ta mong muon no tra ve 1 tap hop ma tung
            //phan tu cua tap hop cua mang do se dua vao bien ket qua, chu khong phai dua ca mang, hay dua ca phan tu tap hop vao
            //bien ket qua. ==> we su dung cau lenh SelectMany . Ktra lai thi bien kq la tap hop cac chuoi chu khong phai mang chuoi 

            //*Min, Max, Sum, Average
            //Cho vao cac tham so cua Min, Max, Sum, Average la cac delegate va chi ra cac phan tu so de no tinh toan 
            //vd: trong danh sach cac gia lay ra gia nho nhat, we viet la trong ds cac sp products, goi pthuc Min(), phthuc Min()
            //nay duoc tinh toan tren cac tap so, nhung tap so nay duoc lay ra tu gia cua san pham
            Console.WriteLine(products.Min(p => p.Price));
               
        }
    }
}
