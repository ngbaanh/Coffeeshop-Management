﻿using CoffeeManagement.BO;
using CoffeeManagement.Models;
using CoffeeManagement.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagement.Views.Popups
{
    public partial class AddUser : Form
    {
        private bool _isUsernameAvailable;
        private UserBo _userBo = new UserBo();
        public AddUser()
        {
            InitializeComponent();
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            _cbLevel.DataSource = Enum.GetNames(typeof(AppEnum.UserLevel));
        }

        private void _btnSave_Click(object sender, EventArgs e)
        {
            if (IsInputValid())
            {
                var user = new User
                {
                    FullName = _tbFullName.Text,
                    UserName = _tbUsername.Text,
                    Password = _tbPassword.Text,
                    Level = (AppEnum.UserLevel)Enum.Parse(typeof(AppEnum.UserLevel), _cbLevel.Text, true),
                    Description = _tbDescription.Text
                };
                _userBo.Add(user);

                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            else
            {
                MessageHelper.CreateErrorMessage("Thông tin nhập vào không hợp lệ!");
            }
        }

        private bool IsInputValid()
        {
            return !(string.IsNullOrEmpty(_tbFullName.Text) || string.IsNullOrEmpty(_tbPassword.Text)) && _isUsernameAvailable;
        }

        private void _tbUsername_Validated(object sender, EventArgs e)
        {
            bool isExist = _userBo.IsUsernameExist(_tbUsername.Text);
            _isUsernameAvailable = !isExist;
            _picNotAvailable.Visible = isExist;
            
        }
    }
}
