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
                new Brand{ID=4, Name="Cong ty CCC"}
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

            Console.WriteLine("............Select...........");
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

            Console.WriteLine("............Where...........");
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

            Console.WriteLine("............Min...........");
            //*Min, Max, Sum, Average
            //Cho vao cac tham so cua Min, Max, Sum, Average la cac delegate va chi ra cac phan tu so de no tinh toan 
            //vd: trong danh sach cac gia lay ra gia nho nhat, we viet la trong ds cac sp products, goi pthuc Min(), phthuc Min()
            //nay duoc tinh toan tren cac tap so, nhung tap so nay duoc lay ra tu gia cua san pham
            Console.WriteLine(products.Min(p => p.Price));

            Console.WriteLine("............Join...........");
            //*Join
            //Pthuc Join ket hop cac nguon du lieu de lay ra san pham phu hop. vd: Moi sp lay ra ten sp va ten hang sx, ten sp nam trong 
            //nguon Product, con hang SX nam trong nguon Brand. Nhu vay moi sp thi phai tra cuu tu 2 nguon.
            //Cach viet: nguon thu nhat la cac san pham, ket hop Join, tham so dau tien la nguon de ket hop, du lieu nao trong products
            //dung de ket hop, du lieu nao trong nguon brands dung de ket hop, kq lay ra thi we dua vao delegate co 2 tham so gom so
            //san pham lay duoc va nhan hieu lay duoc, delegate tra ve dl gi thi no nam trong tap ket qua. o day chung ta tra ve mot
            //doi tuong gom co ten = ten san pham, thuong hieu = ten nhan hieu.

            var kq2 = products.Join(brands, p => p.Brand, b => b.ID, (p, b) =>
            {
                return new
                {
                    Ten = p.Name,
                    Thuonghieu = b.Name
                };
            });

            foreach (var item in kq2)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("............GroupJoin...........");
            //*GroupJoin
            //hoat dong tuong tu nhu Join, tuy nhien nhung phan tu tra ve no la mot cai nhom ma no duoc nhom lai theo cai nguon ban dau
            //vd: we co danh sach nhung nhan hieu, voi moi thuong hieu, liet ke nhung sp thuoc thuong hieu do ra.
            //thuc hien: nguon tao nhom.GroupJoin(tham so thu nhat la nguon nhung phan tu nam trong nhom, tham so tiep theo la du lieu
            //ban dau mang ra tao nhom do la voi moi pham tu b thuoc brand thi lay ra du lieu b.ID, du lieu nay mang ra so sanh voi du
            //lieu nao cua phan tu thuoc nhom, voi moi phan tu p thuoc products thi lay ra Brand. Nhung phan tu p thuoc Product ma co
            //Brand == ID thi tat ca nhung phan tu do se dua vao mot nhom, tham so cuoi cung la mot delegate, delegate nay gom co 2 
            //tham so, tham so thu nhat la nhan hieu lay ra duoc de tao nhom, tham so thu hai la tap hop nhung phan tu thuoc nhom do
            //delegate nay we thiet ke no tra ve nhung phan tu, vd kieu vo danh

            var kq3 = brands.GroupJoin(products, b => b.ID, p => p.Brand,
                (bra, pros) =>
                {
                    return new
                    {
                        Thuonghieu = bra.Name,
                        Cacsanpham = pros
                    };
                }
             );

            foreach (var gr in kq3)
            {
                Console.WriteLine(gr.Thuonghieu);
                foreach (var p in gr.Cacsanpham)
                {
                    Console.WriteLine(p);
                }
            }

            Console.WriteLine("............Take...........");
            //*Take()
            //Phthuc Take() lay ra mot so san pham dau tien. vd: nguon la producs, lay 3 san pham dau tien.
            //In ra, chung ta convert sang List(). vi trong List co phuong thuc foreach, duyet qua tung phan tu, moi phan tu ay
            //la p, chung ta thuc hien ConsoleWrite(p)

            products.Take(5).ToList().ForEach(p => Console.WriteLine(p));

            Console.WriteLine("............Skip...........");
            //*Skip()
            //Phthuc Skip() bo di nhung phan tu dau tien va lay ra nhung phan tu con lai

            products.Skip(2).ToList().ForEach(p => Console.WriteLine(p));

            Console.WriteLine("............OrderBy...........");
            //*OrderBy(tang dan)/OrderByDescending(giam dan): sap xep tang dan/giam dan
            //goi pthuc OrderBy(voi moi sp => lay dlieu nao ra de sap xep). No tra ve mot tap hop cac san pham duoc sap xep tang 
            //dan ve gia. De in ra chuyen ra thanh danh sach, goi pthuc Foreach(voi moi sp => in ra).

            products.OrderBy(p => p.Price).ToList().ForEach(p => Console.WriteLine(p));

            Console.WriteLine("............OrderBy Price...........");
            products.OrderBy(p => p.Price).ToList().ForEach(p => Console.WriteLine(p));

            Console.WriteLine("............OrderByDescending Brand...........");
            products.OrderByDescending(p => p.Brand).ToList().ForEach(p => Console.WriteLine(p));

            Console.WriteLine("............Reverse...........");
            //*Reverse()
            //Phthuc Reverse() dao nguoc cac phan tu trong tap hop
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            numbers.Reverse().ToList().ForEach(n => Console.WriteLine(n));

            Console.WriteLine("............GroupBy...........");
            //*GroupBy()
            //Phthuc GroupBy() tra ve 1 tap hop moi phan tu la mot nhom theo mot du lieu nao do. VD: nhom nhung sp theo gia 
            //Goi pthuc GroupBy(voi moi phan tu trong products => lay du lieu nao ra de nhom, lay gia ra de nhom). Pthuc nay tra
            //ra kq la nhung ptu co kieu IGrouping, moi ptu ay co key, tuong ung voi du lieu de nhom, va trong moi phan tu Grouping 
            //do co tap hop nhung phan tu duoc nhom lai theo nhom. chung ta co the duyet kq nay ra. Voi moi phan tu Grouping, trong
            //phan tu nay co thuoc tinh key, va du lieu de nhom. o day chung ta nhom theo gia, item nay chua nhung sp duoc nhom 
            //theo gia do, theo key, 

            var kq4 = products.GroupBy(p => p.Price);
            foreach (var group in kq4)
            {
                Console.WriteLine(group.Key);
                foreach (var p in group)
                {
                    Console.WriteLine(p);
                }
            }

            Console.WriteLine("............Distinct...........");
            //*Distinct()
            //Pthuc nay loai bo nhung ptu co cung gia tri va giu lay 1 phan tu thoi
            //vd: lay ra mau sac cua tat ca cac sp, su dung pthuc SelectMany(), voi moi san pham lay ra mang mau sac cua no
            //in tat ca mau sac lay ra duoc va loai nhung mau trung nhau.
            products.SelectMany(p => p.Colors).Distinct().ToList().ForEach(mau => Console.WriteLine(mau));

            Console.WriteLine(".................Single..................");
            //*Single() & SingleOrDefault()
            //Single() kiem tra cac phan tu thoa mot dk logic nao do. Neu trong kq co 1 phan tu thoa man dk logic thi tra ve cai phan
            //tu do, neu khong co phan tu nao thoa man dk logic, hoac co nhieu hon 1 phan tu thoa man dk logic thi se phat sinh loi
            //vd: lay ve 1 phan tu single, phan tu do thoa man dk gia = 600, kq cua pthuc nay tra ve 1 phan tu thoi, in ptu day ra
            //trong truong hop khong muon phat sinh loi thi su dung SingleOrDefault(), trong truong hop khong tim thay no se tra ve 
            //kq la null. Trong truong hop tim thay 2 sp thi no van psinh loi
            //SingleOrDefault() chi khac Single() trong truong hop khong tim thay thi no khong phat sinh loi va no tra ve null
            var p1 = products.Single(p => p.Price == 600);
            Console.WriteLine(p1);

            Console.WriteLine(".................SingleOrDefault..................");
            var p2 = products.SingleOrDefault(p => p.Price == 600);
            if(p2!=null) 
            Console.WriteLine(p2);

            Console.WriteLine(".................Any..................");
            //*Any
            //Tra ve true neu thoa man 1 dk logic nao do
            //vd: ktra xem co sp nao co gia = 600 khong. 
            var p3 = products.Any(p => p.Price == 600);
            Console.WriteLine(p3);

            Console.WriteLine(".................All..................");
            //*All
            //Cung tra ve true hoac false. Pthuc all nay la tat cac cac phan tu phai thoa man dk logic
            var p4 = products.All(p => p.Price >= 200);
            Console.WriteLine(p4);

            Console.WriteLine(".................Count()..................");
            //*Count()
            //dem tat ca nhung san pham ma co trong products 
            //pthuc Count() nay cung cho phep nhan tham so la 1 delegate, tra ve true hoac false, tra ve true thi phan tu do duoc dem
            var p5 = products.Count();
            Console.WriteLine(p5);

            var p6 = products.Count(p=>p.Price>=300);
            Console.WriteLine(p6);

            Console.WriteLine(".................VD tong hop API LinQ..................");
            //VD tong hop use API LinQ: Hay in ra ten sp, ten thuong hieu cua sp, chi lay ra nhung sp thoa man dk gia trong khoang
            //300 den 400, kq sap xep theo gia giam dan 
            products.Where(p => p.Price >= 300 && p.Price <= 400)
                .OrderByDescending(p => p.Price)
                .Join(brands, p => p.Brand, b => b.ID, (sp, th) =>
                {
                    return new
                    {
                        TenSP = sp.Name,
                        TenTH = th.Name,
                        Gia = sp.Price
                    };
                })
                .ToList()
                .ForEach(info => Console.WriteLine($"{info.TenSP,15}{info.TenTH,15}{info.Gia,15}"));

            Console.WriteLine(".................Cu phap truy van LinQ..................");
            //1.Xac dinh nguon dl: nguon dl tu nhung doi tuong trien khai IEnumerables, vd: array, List,...
            //  from tenptu(tuy chon) in IEnumerables
            //Nhung bieu thuc loc dl: where, orderby, ...
            //2. nhung chi thi cho biet lay nhung dl nao ra: select, groupby... can cu vao phan tu ma no da chon tu nguon
            //vd: Nguon: nhung sp products, hay lay ra ten sp, 
            //voi moi ptu a trong products, chung ta se lay ra dl gi thi se viet o select, select nay tra ve nhung dtuong duoc
            //tinh toan tren ptu a. kq tre ve 1 tap hop IEnumerable, can convert qr thanh List, trong List co pthuc ForEach
            var qr = from a in products 
                     select a.Name;
            qr.ToList().ForEach(name => Console.WriteLine(name));
            //foreach (var name in qr)
            //{
            //    Console.WriteLine(name);
            //}
            Console.WriteLine("...............tra ve danh sach gom co ten va gia.................");
            var qr1 = from a in products
                      select $"{a.Name}:{a.Price}";
            qr1.ToList().ForEach(name => Console.WriteLine(name));

            Console.WriteLine("...............tra ve 1 doi tuong kieu vo danh................");
            //bang cach su dung toan tu new co the thiet lap no co nhung thuoc tinh gi
            var qr3 = from a in products
                      select new
                      {
                          Ten = a.Name,
                          Gia = a.Price,
                          ABC = "kjkjlk"
                      };
            qr3.ToList().ForEach(name => Console.WriteLine(name));

            Console.WriteLine("............Loc du lieu voi menh de where..............");
            //vd Lay nhung sp co gia =400
            var qr4 = from product in products
                      where product.Price == 400
                      select new
                      {
                          Ten = product.Name,
                          Gia = product.Price
                      };
            qr4.ToList().ForEach(abc=> Console.WriteLine(abc));

            Console.WriteLine(".............nhieu menh de from trong linq....................");
            //co the xac dinh nhieu nguon bang cach xac dinh nhieu from
            //vd lay ra nhung sp co gia <=500 va co mau xanh
            //xd nguon 1: nhung sp tu products
            //nguon 2: nhung mau sac tu nguon 1(trong Color cua sp do)
            //luc do o nhung cau lenh tiep theo we can su dung 2 bien product va color nay
            //cac mau nam trong mang chuoi

            var qr5 = from product in products
                      from color in product.Colors

                      where product.Price <= 500 && color == "xanh"

                      select new
                      {
                          Ten = product.Name,
                          Gia = product.Price,
                          Cacmau = product.Colors
                      };
            qr5.ToList().ForEach(abc => Console.WriteLine($"{abc.Ten} - {abc.Gia} - {string.Join(',', abc.Cacmau)}"));

            Console.WriteLine("...............OrderBy..................");
            //neu muon sap xep ket qua tra ve thi thuc hien OrderBy truoc Select, sau Where
            //sap xep tang dan thi de nguyen, giam dan thi them tu khoa descending 

            var qr6 = from product in products
                      from color in product.Colors

                      where product.Price <= 500 && color == "xanh"

                      orderby product.Price descending

                      select new
                      {
                          Ten = product.Name,
                          Gia = product.Price,
                          Cacmau = product.Colors
                      };
            qr6.ToList().ForEach(a => Console.WriteLine($"{a.Ten} - {a.Gia} - {string.Join(',', a.Cacmau)}"));

            Console.WriteLine("............Nhom ket qua voi GroupBy...............");
            //GroupBy nhom nhung ket qua phia truoc lai thanh tung nhom theo 1 thuoc tinh nao do va no tra ve 1 tap hop gom nhung
            //phan tu, nhung phan tu day la 1 group, trong group day do key theo thuoc tinh de nhom va nhung phan tu thoa man key do
            //vd: we co dsach cac sp, hay nhom nhung sp day theo gia, vd gia 400 co bao nhieu sp 

            //bieu thuc kq select hoac group we can luu no thanh 1 cai bien tam, sau do we thuc hien nhung tac vu tren bien tam do
            //roi moi tra ve nhung phan tu 
            //nhung phan tu nhom sp theo gia khong tra ve ngay ma no se luu thanh mot cai bien tam, viet into, kq luu vao 1 cai 
            //bien tam la gr

            var qr7 = from p in products
                      group p by p.Price into gr
                      orderby gr.Key
                      select gr;
            qr7.ToList().ForEach(group =>
            {
                Console.WriteLine(group.Key);
            group.ToList().ForEach(p => Console.WriteLine(p));
            });

            //trong cau truy van linq chung ta co the khai bao ra mot bien bang tu khoa let tenbien va gan bang mot bieu thuc 
            //tinh toan nao do 
            //vd: truy van va tra ve cac doi tuong co nhung thuoc tinh Gia = gia san pham, Cacsanpham gom nhom cac sp co gia bang
            //thuoc tinh gia, Soluong cho biet cac san pham co gia co bao nhieu san pham 
            //khi khai bao bien va gan bien cho mot bieu thuc, sau do we can su dung duoc bien do

            var qr8 = from p in products
                      group p by p.Price into gr
                      orderby gr.Key
                      let sl = "So luong la: " + gr.Count()
                      select new
                      {
                          Gia = gr.Key,
                          Cacsanpham = gr.ToList(),
                          Soluong = sl
                      };
            qr8.ToList().ForEach(i => {
                Console.WriteLine(i.Gia);
                Console.WriteLine(i.Soluong);
                i.Cacsanpham.ForEach(p => Console.WriteLine(p));
            });

            Console.WriteLine("...................Join..................");
            //ket hop giua cac nguon dl trong cau truy van linq 
            //vd: ket hop nguon dl la products va brands. nhiem vu liet ke cac cac san pham: ten, ten hang sx, gia sp 
            //nguon1: product trong products ket noi voi nguon 2, dk ket noi su dung tu khoa on thuoc tinh nao trong product 
            //bang thuoc tinh nao trong brand 
            //nhu inner join trong sql 
            var qr9 = from product in products
                      join brand in brands on product.Brand equals brand.ID
                      select new
                      {
                          Ten = product.Name,
                          Gia = product.Price,
                          Thuonghieu = brand.Name
                      };
            qr9.ToList().ForEach(o => Console.WriteLine($"{o.Ten,10} {o.Thuonghieu,15}{o.Gia,5}"));

            Console.WriteLine("......truong hop muon lay ra tat ca sp, ke ca sp day khong xac dinh nhan hieu trong brand....");
            //lam nhu sau: nhung brand dung de khop noi voi product we luu ra bien tam nao do 
            //sau do we xac dinh nguon dl thu 2 la nhung nhan hieu b dang luu trong t, neu nhan hieu do khong luu duoc sp trong t
            //thi no se tra ve null 
            var qr10 = from product in products
                       join brand in brands on product.Brand equals brand.ID into t
                       from b in t.DefaultIfEmpty()
                       select new
                       {
                           Ten = product.Name,
                           Gia = product.Price,
                           Thuonghieu = (b!=null)?b.Name:"No brand"
                       };
            qr10.ToList().ForEach(o => Console.WriteLine($"{o.Ten,10}{o.Gia,5}{o.Thuonghieu,15}"));





        }


    }
}
