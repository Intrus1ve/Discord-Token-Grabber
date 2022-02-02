using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Manav_Otomasyonu
{
    using Repository;
    using Entities;
    public partial class CustomerForm : Form
    {
        CustomerRepo cr;
        Customer SelectedCustomer = null;
        UlkeRepo ur;
        SehirRepo sr;
        IlceRepo ir;
        public CustomerForm()
        {
            InitializeComponent();
            cr = new CustomerRepo();
            ur = new UlkeRepo();
            sr = new SehirRepo();
            ir = new IlceRepo();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            FillControl();
            FillData();
        }

        private void FillControl()
        {
            cmbCountry.DataSource = ur.Get();
            cmbCountry.DisplayMember = "UlkeAdi";
            cmbCountry.ValueMember = "UlkeAdi";
            cmbSehirler.DataSource = sr.Get();
            cmbSehirler.DisplayMember = "SehirAdi";
            cmbSehirler.ValueMember = "SehirId";
            cmbIlce.DataSource = ir.Get();
            cmbIlce.DisplayMember = "IlceAdi";
            cmbIlce.ValueMember = "IlceId";
        }

        private void FillData()
        {
            int Id = Convert.ToInt32(this.Tag);
            if (Id>0)
            {
                Customer customer = cr.GetById(Id);
                if (customer!=null)
                {
                    SelectedCustomer = customer;

                    txtFirstName.Text = customer.FirstName;
                    txtLastName.Text = customer.LastName;
                    cmbCountry.SelectedValue = customer.Country;
                    cmbSehirler.SelectedItem = customer.City;
                    cmbIlce.SelectedItem = customer.Region;
                    txtPostalCode.Text = customer.PostalCode;
                    txtPhone.Text = customer.Phone;
                    txtAddress.Text = customer.Address;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FormSave();
            Close();
        }

        private void FormSave()
        {
            if (SelectedCustomer==null)
            {
                SelectedCustomer = new Customer();
            }
            SelectedCustomer.FirstName = txtFirstName.Text;
            SelectedCustomer.LastName = txtLastName.Text;
            SelectedCustomer.Country = (cmbCountry.SelectedValue).ToString();
            SelectedCustomer.City = (cmbSehirler.SelectedItem as Sehir).SehirAdi;
            SelectedCustomer.Region = (cmbIlce.SelectedItem as Ilce).IlceAdi;
            SelectedCustomer.PostalCode = txtPostalCode.Text;
            SelectedCustomer.Phone = txtPhone.Text;
            SelectedCustomer.Address = txtAddress.Text;

            if (Convert.ToInt32(this.Tag) == 0)
            {
                SelectedCustomer.MusteriID = cr.Create(SelectedCustomer);
                this.Tag = SelectedCustomer.MusteriID;
            }
            else
            {
                cr.Update(SelectedCustomer);
            }
        }

        private void txtPhone_Click(object sender, EventArgs e)
        {
            this.txtPhone.Select(1, 0);
        }

        private void cmbSehirler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSehirler.SelectedIndex==-1)
            {
                return;
            }
            Sehir sehir = (cmbSehirler.SelectedItem as Sehir);
            int id = sehir.SehirId;
            FillIlce(id);
            
        }

        private void FillIlce(int id)
        {
            cmbIlce.DataSource = ir.GetBySehirId(id);
            cmbIlce.DisplayMember = "IlceAdi";
            cmbIlce.ValueMember = "IlceId";

        }
    }
}
