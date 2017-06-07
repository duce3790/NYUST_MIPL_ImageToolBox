﻿using ImageProcessToolBox.MedicalImageFinal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessToolBox
{
    public partial class FormMedicalImageFinal : Form
    {
        private Bitmap _imageSource;
        public FormMedicalImageFinal(Bitmap imgSrc)
        {
            InitializeComponent();
            _imageSource = imgSrc;

            init();
            process();
        }
        private Label[] labels;
        private PictureBox[] imageShow;
        private String[] labelsStr = { "Step1:", "Step2:", "Step3:", "Step4:" };
        
        private IImageProcess[] iProcess = { new Transfor(25), new SpiltImage(), new LaplacianBG() };
        private static int STEP_SIZE = 3;

        private void init()
        {
            labels = new Label[]{ label1, label2, label3, label4, label5, label6, label7 };
            imageShow = new PictureBox[]{ pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8 };
        }
        private void process()
        {
            Bitmap src = _imageSource;
            for (int i = 0; i < STEP_SIZE; i++)
            {
                labels[i].Text = labelsStr[i];
                src = Process(src, iProcess[i], imageShow[i]);
            }
            step4(src);

        }

        private Bitmap step4(Bitmap src)
        {
            label4.Text = "Step 4 : ";

            IImageProcess process = new MorphologyDilation();
            process.setResouceImage(src);
            return Process(src, process, pictureBox4);
        }

        private Bitmap stepTest(Bitmap src)
        {
            return null;
        }

        private Bitmap Process(Bitmap src, IImageProcess Iprocess, PictureBox show)
        {
            Iprocess.setResouceImage(src);
            Bitmap res = Iprocess.Process();
            show.Image = res;
            return res;
        }
    }
}