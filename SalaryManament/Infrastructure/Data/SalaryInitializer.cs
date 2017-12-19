using System;
using System.Collections.Generic;
using System.Data.Entity;
using Core.DTO;

namespace Infrastructure.Data
{
    class SalaryInitializer : DropCreateDatabaseIfModelChanges<SalaryContext>
    {
        protected override void Seed(SalaryContext context)
        {
 	         base.Seed(context);
             var positions = new List<chucvu> {
                 new chucvu{id=1,chuc_vu="Hiệu trưởng",he_so=0.8f},
                 new chucvu{id=1,chuc_vu="Hiệu phó",he_so=0.7f},
                 new chucvu{id=1,chuc_vu="Trưởng phòng",he_so=0.5f},
                 new chucvu{id=1,chuc_vu="Trưởng khoa",he_so=0.5f},
                 new chucvu{id=1,chuc_vu="Giám đốc trung tâm",he_so=0.5f},
                 new chucvu{id=1,chuc_vu="Phó phòng",he_so=0.4f},
                 new chucvu{id=1,chuc_vu="Phó khoa",he_so=0.4f},
                 new chucvu{id=1,chuc_vu="Phó giám đốc trung tâm",he_so=0.4f},
                 new chucvu{id=1,chuc_vu="Trưởng bộ môn trực thuộc khoa",he_so=0.4f},
                 new chucvu{id=1,chuc_vu="Phó bộ môn trực thuộc khoa",he_so=0.3f},
             };
             positions.ForEach(p => context.chucvu.Add(p));
             context.SaveChanges();
             var salary = new List<luong> {
                 new luong{id=1,dinhmuc=720000},
             };
             salary.ForEach(s => context.luong.Add(s));
             context.SaveChanges();
             var ranks = new List<ngach> {
                 new ngach{id=1,maso="01.001",ngach1="Chuyên viên cao cấp",nien_hang=3,C_1="6,20",C_2="6.56",C_3="6.92",C_4="7.28",C_5="7.64",C_6="8.00",C_7="8.36",C_8="VK 0.05"},
                 new ngach{id=2,maso="01.003",ngach1="Chuyên viên",nien_hang=3,C_1="2.34",C_2="2.67",C_3="3.00",C_4="3.33",C_5="3.66",C_6="3.99",C_7="4.32",C_8="VK 0.05"},
                 new ngach{id=3,maso="15.110",ngach1="Giảng viên chính",nien_hang=3,C_1="4.40",C_2="4.74",C_3="5.08",C_4="5.42",C_5="5.76",C_6="6.10",C_7="6.44",C_8="VK 0.05"},
                 new ngach{id=4,maso="15.111",ngach1="Giảng viên",nien_hang=3,C_1="2.34",C_2="2.67",C_3="3.00",C_4="3.33",C_5="3.60",C_6="3.99",C_7="4.32",C_8="VK 0.05"},
                 new ngach{id=5,maso="01.004",ngach1="Cán sự",nien_hang=2,C_1="1.86",C_2="2.06",C_3="2.26",C_4="2.46",C_5="2.66",C_6="VK 0,07",C_7="VK 0,09",C_8="VK 0,11"},
                 new ngach{id=6,maso="01.007",ngach1="Nhân viên kỹ thuật",nien_hang=2,C_1="1.65",C_2="1.83",C_3="2.01",C_4="2.19",C_5="2.37",C_6="VK 0,07",C_7="VK 0,09",C_8="VK 0,11"},
                 new ngach{id=7,maso="01.11",ngach1="Nhân viên bảo vệ",nien_hang=2,C_1="1.50",C_2="1.68",C_3="1.86",C_4="2.04",C_5="2.22",C_6="VK 0,07",C_7="VK 0,09",C_8="VK 0,11"},
             };
             ranks.ForEach(r => context.ngach.Add(r));
             context.SaveChanges();
        }
    }
}
