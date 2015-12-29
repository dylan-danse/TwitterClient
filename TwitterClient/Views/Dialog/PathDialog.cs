﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TwitterClient.Views.Dialog
{
    public class PathDialog
    {
        public event EventHandler OnOpenDialog;
        public event EventHandler<PathDialogEventArgs> OnPathChanged;

        public void OpenPathDialog()
        {
            CallHandler(OnOpenDialog, EventArgs.Empty);

            SaveFileDialog saveFileDialog;
            saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = @"C:\";

            if (saveFileDialog.ShowDialog() == true)
            {
                EventHandler<PathDialogEventArgs> handler = OnPathChanged;
                if (handler != null)
                {
                    handler(this, new PathDialogEventArgs { Path = saveFileDialog.FileName });
                }
            }
        }

        protected virtual void CallHandler(EventHandler handler, EventArgs args)
        {
            if (handler != null)
            {
                handler(this, args);
            }
        }   
    }
}
