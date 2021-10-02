using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;  //원화표시 포맷 추가

namespace kiosk_sample2
{
    /* public class MENU  //메뉴 리스트를 위한 3개항목 설정
     {
         public string Menu { get; set; }
         public int Num { get; set; }
         public int Price { get; set; }
     }
    */
    public partial class Form1 : Form
    {
        // List<MENU> myMenu = null; //리스트를 사용하기 위한 클래스 메뉴 불러오기
        Dictionary<string, int> cost = new Dictionary<string, int>();
        //이 함수는 첫번째 항목(키)을 검색하면 뒤의 항목(값)을 가져온다.
        int count = 1; // 주문번호이므로 초기화 해도 1씩 늘어나야한다.
        NumberFormatInfo numberFormat = new CultureInfo("ko-KR", false).NumberFormat;  //원화 표시 포맷 추가
               
        public Form1()
        {
            InitializeComponent();
            setCost(); //가격 설정을 해준다. 한번만
        }
        private void button1_Click(object sender, EventArgs e) // 한국어 버튼 클릭
        {
            panel1.Visible = true;    //판넬을 추가하고 visible 속성을 flase 로 해놔야 한다.
        }
        private void button2_Click(object sender, EventArgs e) // 영어 버튼 클릭
        {
            panel1.Visible = true;
        }
        private void button3_Click(object sender, EventArgs e) //2번째페이지의 이전버튼으로 언어 선택 창으로 이동
        {
            panel1.Visible = false;
        }
        private void button4_Click(object sender, EventArgs e) //2번째페이지의 다음버튼으로 결제 창으로 이동
        {
            panel2.Visible = true;
        }
        private void button5_Click(object sender, EventArgs e)  //3번째페이지의 이전버튼으로 메뉴선택 창으로 이동
        {
            panel2.Visible = false;
        }
        private void button27_Click(object sender, EventArgs e)  //포장 클릭
        {
            panel3.Visible = true;
            textBox21.Text = "포장";   //포장상태
            textBox28.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //현재시간
            textBox22.Text = count.ToString(); // 주문번호에 카운트 입력
        }
        private void button28_Click(object sender, EventArgs e) // 매장클릭
        {
            panel3.Visible = true;
            textBox21.Text = "매장";   //포장상태
            textBox28.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");  //현재시간
            textBox22.Text = count.ToString();  // 주문번호에 카운트 입력
        }
        private void button6_Click(object sender, EventArgs e)   //3번째페이지의 다음 버튼으로 결제 완료 창으로 이동
        {
            panel3.Visible = true;
        }
        private void button31_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }
        private void button29_Click(object sender, EventArgs e) //현금 클릭
        {
            count++;  // 주문번호가 1씩 증가
            panel4.Visible = true;            
        }
        private void button30_Click(object sender, EventArgs e)  //카드 클릭
        {
            count++;  // 주문번호가 1씩 증가
            panel4.Visible = true;
        }
        private void button7_Click(object sender, EventArgs e)  // 결제 완료창의 처음 버튼으로 맨처음 언어 선택 화면으로 이동
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            dataGridView1.Rows.Clear(); //처음으로 가기 때문에 다 비워 줘야한다.
            dataGridView2.Rows.Clear();
            textBox20.Clear();
            textBox21.Clear();
            textBox25.Clear();
            tabControl1.SelectedTab = tabPage1; 
        }        
        private void setCost() //메뉴 이름과 가격 설정한다.   Dictionary 사용
        {                      // 이런식으로 항목을 추가하면 리스트로해서 인덱스마다 찾기보다 편함
            cost.Add("떡볶이", 3500);
            cost.Add("치즈 떡볶이", 4500);
            cost.Add("로제 떡볶이", 5000);
            cost.Add("오징어튀김(2개)", 2000);
            cost.Add("김말이(3개)", 1500);
            cost.Add("튀김오뎅(6개)", 1500);
            cost.Add("튀김만두(5개)", 1500);
            cost.Add("한입고추튀김(3개)", 2000);
            cost.Add("잡채말이(3개)", 2000);
            cost.Add("미니핫도그(2개)", 1500);
            cost.Add("치즈스틱(3개)", 2000);
            cost.Add("참치마요컵밥", 3500);
            cost.Add("스팸마요컵밥", 3500);
            cost.Add("신전라이스", 3500);
            cost.Add("신전김밥", 2500);
            cost.Add("콜라", 2500);
            cost.Add("사이다", 2500);
            cost.Add("쿨피스", 2000);
            cost.Add("웰치스", 1500);
        }
        private int getCost(string menuName)   //가격을 가져온다. 메뉴를 선택하면
        {
            return cost[menuName];
        }
        private DataGridViewRow NameCheck(string menuName, string check = null) // menuName 과 DataGridView의 메뉴이름과 같은지 찾는 매소드
                                                                                //메뉴이름          //맵기 값(초기값은 null)
        {
            if (check != null) menuName = check + " " + menuName; //맵기값이 null 이아니면 menuName = 맵기+menuName

            foreach (DataGridViewRow row in dataGridView1.Rows) // row 전체를 나열하기위한 반복문
            {
                if (dataGridView1.Rows.IndexOf(row) != dataGridView1.RowCount - 1) //디자인에 항상표시되는 마지막 빈 행을 제거해야 검색이 된다. 아니면 자꾸 에러 가 뜬다.
                {
                    if (row.Cells[0].Value.ToString() == menuName)  //첫번째 행이 메뉴네임과 같을때
                    {
                        return row;  //row를 리턴한다. true 받으면 수식하기힘듬
                    }
                }
            }
            return null;  // null 값을 받는다
        }
        private DataGridViewRow NameCheck2(string menuName, string check = null) // 영수증의 그리드뷰 값
        {
            if (check != null) menuName = check + " " + menuName;

            foreach (DataGridViewRow row2 in dataGridView2.Rows)
            {
                if (dataGridView2.Rows.IndexOf(row2) != dataGridView2.RowCount - 1)
                {
                    if (row2.Cells[0].Value.ToString() == menuName)
                    {
                        return row2;
                    }
                }
            }
            return null;  // null 값을 받는다
        }
        private void menuItemClick(object sender, EventArgs e) //떡볶이를 제외한 모든 메뉴 클릭 이벤트
        {
            string menuName = (sender as Button).Text.Trim(); // menuName = 클릭한 버튼의 Text 값
            int menuCost = getCost(menuName); // menuCost = getcost를 불러옴 return 값이 cost[menuName]이니깐 dictionary 함수에 적용되서 int 값이 나온다.

            DataGridViewRow row = NameCheck(menuName); //menuName 과 같은 값을 가진 행 row 를 찾는다
            if ( row != null ) //null 값이 아니면 행이있다는것이므로 수량과 가격을 늘린다.
            {
                row.Cells[1].Value = Convert.ToInt32(row.Cells[1].Value) + 1;        //수량 행의 값을 1 더한다.
                row.Cells[2].Value = Convert.ToInt32(row.Cells[1].Value) * menuCost; // 가격행을 수량과 가격을 곱한다.
            } else  //null 값이므로 행이 없으므로 행을 추가한다.
            {
                dataGridView1.Rows.Add(menuName, 1, menuCost);
            }

            //영수증의 그리드뷰 값
            DataGridViewRow row2 = NameCheck2(menuName); 
            if (row2 != null) 
            {
                row2.Cells[1].Value = Convert.ToInt32(row.Cells[1].Value) + 1;     
                row2.Cells[2].Value = Convert.ToInt32(row.Cells[1].Value) * menuCost; 
            }
            else
            {
                dataGridView2.Rows.Add(menuName, 1, menuCost);
            }

            TotalSum(); //합계를 표시
        }
        private void menuItemClick2(object sender, EventArgs e) //떡볶이를 위한 메뉴 클릭 이벤트
        {
            hot_level form2 = new hot_level(); //맵기 조절 폼 부르기
            form2.Owner = this;      // form2의 부모 폼은 this(나다).
            form2.ShowDialog();       // form2를 불러온다(모달상태=현재폼만 클릭가능)

            if (form2.check != null)   // form2에서 체크값이 null 이아니라면(즉 클릭했다면)
            {
                string menuName = (sender as Button).Text.Trim();    // menuName = 클릭한 버튼의 Text 값
                int menuCost = getCost(menuName);  // menuCost = getcost를 불러옴 return 값이 cost[menuName]이니깐 dictionary 함수에 적용되서 int 값이 나온다.

                DataGridViewRow row = NameCheck(menuName, form2.check); //menuName 과 같은 값을 찾는다
                if (row != null) //null 값이 아니면 행이있다는것이므로 수량과 가격을 늘린다.
                {
                    row.Cells[1].Value = Convert.ToInt32(row.Cells[1].Value) + 1;  //수량 행의 값을 1 더한다.
                    row.Cells[2].Value = Convert.ToInt32(row.Cells[1].Value) * menuCost; // 가격행을 수량과 가격을 곱한다.
                }
                else  //null 값이므로 행이 없으므로 행을 추가한다.
                {
                    dataGridView1.Rows.Add(form2.check + " " + menuName, 1, menuCost);
                }

                //영수증의 그리드뷰 값 
                DataGridViewRow row2 = NameCheck2(menuName, form2.check);
                if (row2 != null)
                {
                    row2.Cells[1].Value = Convert.ToInt32(row2.Cells[1].Value) + 1;
                    row2.Cells[2].Value = Convert.ToInt32(row2.Cells[1].Value) * menuCost;
                }
                else
                {
                    dataGridView2.Rows.Add(form2.check + " " + menuName, 1, menuCost);
                }

                TotalSum(); //합계를 표시
            }
        }
        private void TotalSum()   //합계를 매소드로 표현 (3군데 붙여야해서 매소드로 만듬 = 메뉴아이템클릭1,2,삭제누를때)
        {
            int sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);
            }
            textBox20.Text = sum.ToString("c", numberFormat);  //원화 표시 해주는 포맷 추가
            textBox25.Text = sum.ToString("c", numberFormat);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);// e 이벤트  셀을 클릭했을때 해당열의 위치찾고 그열을 삭제
            dataGridView2.Rows.Remove(dataGridView2.Rows[e.RowIndex]);
            TotalSum();  //합계를 표시 
        }
        //private void button8_Click(object sender, EventArgs e) //떡볶이 클릭
        //{
        //    hot_level form2 = new hot_level(); //맵기 조절 폼 부르기
        //    form2.Owner = this;    // form2의 부모 폼은 this 나다.
        //    form2.ShowDialog();    // form2를 보여줘
        //    if (form2.check != null)  // form2의 check 상수가 null 값이 아니라면
        //    {
        //        dataGridView1.Rows.Add(form2.check + " 떡볶이", 1, 3500);  //그리드 뷰에 행을 추가해라 check + 떡볶이  , 수량 , 가격
        //    }
        //}        
        // // 버튼 클릭을 하나하나 지정하면 코드가 엄청 길어지고 각 버튼마다 설정해줘야한다. 위의 메뉴 아이템 클릭을 만들어서 한번에 해결
        //private void button12_Click(object sender, EventArgs e) //김말이 튀김 클릭
        //{
        //    dataGridView1.Rows.Add("김말이(3개)", 1, 1500);
        //}        
        //public void eventclick(object sender , EventArgs e) 
        // {
        //     dataGridView1.Rows.Add(myMenu[3].Menu, myMenu[3].Num, myMenu[3].Price); // 리스트를 이용해서 항목을 추가하는 방법 
        // }        
        //private void Form1_Load(object sender, EventArgs e)    // 리스트를 이용해서 입력하려 했으나 항목항목 다 설정해줘야한다. 
        //{
        //    myMenu = new List<MENU>();
        //    myMenu.Add(new MENU { Menu = "떡볶이", Num = 1, Price = 3500 });
        //    myMenu.Add(new MENU { Menu = "치즈 떡볶이", Num = 1, Price = 4500 });
        //    myMenu.Add(new MENU { Menu = "로제 떡볶이", Num = 1, Price = 5000 });
        //    myMenu.Add(new MENU { Menu = "오징어 튀김(2개)", Num = 1, Price = 2000 });
        //    myMenu.Add(new MENU { Menu = "김말이(3개)", Num = 1, Price = 1500 });
        //    myMenu.Add(new MENU { Menu = "튀김오뎅(6개)", Num = 1, Price = 1500 });
        //    myMenu.Add(new MENU { Menu = "튀김만두(5개)", Num = 1, Price = 1500 });
        //    myMenu.Add(new MENU { Menu = "한입고추튀김(3개)", Num = 1, Price = 2000 });
        //    myMenu.Add(new MENU { Menu = "잡채말이(3개)", Num = 1, Price = 2000 });
        //    myMenu.Add(new MENU { Menu = "미니핫도그(2개)", Num = 1, Price = 1500 });
        //    myMenu.Add(new MENU { Menu = "치즈스틱(3개)", Num = 1, Price = 2000 });
        //    myMenu.Add(new MENU { Menu = "참치마요컵밥", Num = 1, Price = 3500 });
        //    myMenu.Add(new MENU { Menu = "스팸마요컵밥", Num = 1, Price = 3500 });
        //    myMenu.Add(new MENU { Menu = "신전라이스", Num = 1, Price = 3500 });
        //    myMenu.Add(new MENU { Menu = "신전 김밥", Num = 1, Price = 2500 });
        //    myMenu.Add(new MENU { Menu = "콜라", Num = 1, Price = 2500 });
        //    myMenu.Add(new MENU { Menu = "사이다", Num = 1, Price = 2500 });
        //    myMenu.Add(new MENU { Menu = "쿨피스", Num = 1, Price = 2000 });
        //    myMenu.Add(new MENU { Menu = "웰치스", Num = 1, Price = 1500 });
        //}
    }
}