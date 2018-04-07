using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Drawing.Imaging;
using System.Data.SQLite;

namespace BitManage.MidControler
{
    public partial class ContentControl : UserControl
    {
        public ContentControl()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            ToolTip tip = new ToolTip();
            tip.SetToolTip(btnUpload, "上传数据");
        }
        /// <summary>
        /// 设置显示图片信息
        /// </summary>
        /// <param name="bitpaths"></param>
        public void SetBitContent(string[] bitpaths)
        {
            if (_pathArray != null && _pathArray[0].Equals(bitpaths[0]))
            {
                _pathArray = bitpaths;
                return;
            }
            //第一次进来时加载配置文件
            if (_pathArray == null)
            {
                SetComboArray();
            }

            BitInfo info = GetBitInfo(bitpaths[0]);
            if (this.pg_context.SelectedObject != null)
            {
                this.pg_context.SelectedObject = null;
            }
            this.pg_context.Refresh();
            this.pg_context.SelectedObject = info;
            _pathArray = bitpaths;
        }

        private BitInfo GetBitInfo(string bitPath)
        {
            BitInfo bitinfo = null;
            SqliteHelper.SqliteHelper mysql = new SqliteHelper.SqliteHelper();
            if (!mysql.DBConnect(_localDBname))
            {
                throw new Exception("连接数据库失败");
            }

            string sql = "select * from u_picture where u_picture.picture_file='" + bitPath + "';";
            DataTable bitDt = mysql.DBReadTable(sql);
            mysql.DBDisConnect();

            if (bitDt == null)
            {
                throw new Exception("查询数据库失败");
            }
            else if (bitDt.Rows.Count == 0)
            {
                bitinfo = new BitInfo();
                GetBitmapInfo(bitPath, bitinfo);
            }
            else if (bitDt.Rows.Count == 1)
            {
                BitInfo[] bitinfos = DataTableConvert.TableToT<BitInfo>(bitDt);
                bitinfo = bitinfos[0];
            }
            else
            {
                throw new Exception("数据库数据有误，存在多条相同的记录");
            }
            return bitinfo;
        }

        private void GetBitmapInfo(string path, BitInfo bitinfo)
        {
            bitinfo.路径 = path;
            if (File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
                bitinfo.图片大小 = fs.Length.ToString() + "byte";
                fs.Close();
                fs.Dispose();

                Bitmap bit = new Bitmap(path);
                bitinfo.图片宽 = bit.Width.ToString();
                bitinfo.图片高 = bit.Height.ToString();
                bitinfo.图片格式 = Path.GetExtension(path).Substring(1);
                bitinfo.垂直分辨率 = bit.VerticalResolution.ToString();
                bitinfo.水平分辨率 = bit.HorizontalResolution.ToString();
                bitinfo.位深度 = GetBitDepth(bit.PixelFormat).ToString();
                bit.Dispose();
                bit = null;
                bitinfo.gps信息 = fnGPS坐标(path);
            }
        }
        /// <summary>
        /// 获取位深度
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        private int GetBitDepth(PixelFormat format)
        {
            switch (format)
            {
                case PixelFormat.Format24bppRgb:
                case PixelFormat.Format32bppArgb:
                case PixelFormat.Format32bppPArgb:
                case PixelFormat.Format32bppRgb:
                case PixelFormat.Format8bppIndexed:
                    return 8;
                case PixelFormat.Format16bppArgb1555:
                case PixelFormat.Format16bppGrayScale:
                case PixelFormat.Format16bppRgb555:
                case PixelFormat.Format16bppRgb565:
                case PixelFormat.Format48bppRgb:
                case PixelFormat.Format4bppIndexed:
                case PixelFormat.Format64bppArgb:
                case PixelFormat.Format64bppPArgb:
                    return 16;
                case PixelFormat.Format1bppIndexed:
                    return 1;
                default:
                    return 8;
            }
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_MouseUp(object sender, MouseEventArgs e)
        {
            //未选中图片时点击上传
            if (_pathArray == null)
            {
                return;
            }
            SqliteHelper.SqliteHelper mysql = new SqliteHelper.SqliteHelper();
            try
            {
                btnUpload.Enabled = false;

                if (_pathArray.Length == 1)
                {
                    BitInfo bitinfo = this.pg_context.SelectedObject as BitInfo;
                    if (!mysql.DBConnect(_localDBname))
                    {
                        throw new Exception("连接数据库失败");
                    }
                    string res = InsertDB(_pathArray[0], bitinfo, mysql);
                }
                else if (_pathArray.Length > 1)
                {
                    BitInfo bitinfo = this.pg_context.SelectedObject as BitInfo;
                    BitInfo[] bitInfoArray = new BitInfo[_pathArray.Length];
                    for (int i = 0; i < _pathArray.Length; i++)
                    {
                        bitInfoArray[i] = bitinfo.Clone();
                        GetBitmapInfo(_pathArray[i], bitInfoArray[i]);
                    }

                    if (!mysql.DBConnect(_localDBname))
                    {
                        throw new Exception("连接数据库失败");
                    }
                    string res = string.Empty;
                    for (int i = 0; i < _pathArray.Length; i++)
                    {
                        res = InsertDB(_pathArray[0], bitInfoArray[i], mysql);
                        if (!string.IsNullOrEmpty(res))
                        {
                            throw new Exception(res);
                        }
                    }
                }
                else
                {
                    throw new Exception("数据库数据有误，存在多条相同的记录");
                }
                MessageBox.Show("上传成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnUpload.Enabled = true;
                mysql.DBDisConnect();
            }
        }
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownload_MouseUp(object sender, MouseEventArgs e)
        {
            if (_pathArray == null)
            {
                return;
            }
            FolderBrowserDialog gbd = new FolderBrowserDialog();
            if (gbd.ShowDialog() == DialogResult.OK)
            {
                string path = gbd.SelectedPath + "\\记录表_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                SqliteHelper.SqliteHelper mysql = new SqliteHelper.SqliteHelper();
                if (!mysql.DBConnect(_localDBname))
                {
                    MessageBox.Show("查询数据库失败");
                }
                try
                {
                    string content = string.Join("','", _pathArray);
                    string sql = "select picture_name as 文件名称, picture_file as 路径, picture_title as 标题,picture_time as 时间,picture_author as 作者,picture_level as 评级,picture_info as 摘要,picture_key as 关键词,picture_person as 人物,picture_org as 机构,picture_type as 分类,picture_marker as 标引人,picture_marktime as 标引时间,picture_width as 图片宽,picture_height as 图片高,picture_hr as 水平分辨率,picture_vr as 垂直分辨率,picture_bit as 位深度,picture_gps as gps信息,picture_size as 图片大小,picture_format as 图片格式 from u_picture where picture_file in ('" + content + "')";
                    DataTable dt = mysql.DBReadTable(sql);
                    if (dt == null)
                    {
                        throw new Exception("查询数据库失败");
                    }
                    bool res = ExcelHandler.Write(path, dt);
                    if (!res)
                    {
                        throw new Exception("导出表格失败");
                    }
                    else
                    {
                        MessageBox.Show("导出成功");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    mysql.DBDisConnect();
                }
            }
        }

        private string InsertDB(string bitPath, BitInfo info, SqliteHelper.SqliteHelper mysql)
        {
            string sql = "select * from u_picture where u_picture.picture_file='" + bitPath + "';";
            DataTable dt = mysql.DBReadTable(sql);
            if (dt == null)
            {
                return "连接数据库失败";
            }
            else if (dt.Rows.Count == 0)
            {
                sql = "insert into u_picture(picture_name,picture_file,picture_title,picture_time,picture_author,picture_level,picture_info,picture_key,picture_person,picture_org,picture_type,picture_marker,picture_marktime,picture_width,picture_height,picture_hr,picture_vr,picture_bit,picture_gps,picture_size,picture_format)values('" + info.文件名称 + "','" + bitPath + "','" + info.标题 + "','" + info.时间 + "','" + info.作者 + "','" + info.评级 + "','" + info.摘要 + "','" + info.关键词 + "','" + info.人物 + "','" + info.机构 + "','" + info.分类 + "','" + info.标引人 + "','" + info.标引时间 + "','" + info.图片宽 + "','" + info.图片高 + "','" + info.水平分辨率 + "','" + info.垂直分辨率 + "','" + info.位深度 + "','" + info.gps信息 + "','" + info.图片大小 + "','" + info.图片格式 + "');";
                int res = mysql.DBUpdateInsert(sql);
                if (res == -1)
                {
                    return "写入数据失败";
                }
            }
            else if (dt.Rows.Count == 1)
            {
                sql = "update u_picture set picture_name='" + info.文件名称 + "',picture_title='" + info.标题 + "',picture_time='" + info.时间 + "',picture_author='" + info.作者 + "',picture_level='" + info.评级 + "',picture_info='" + info.摘要 + "',picture_key='" + info.关键词 + "', picture_person='" + info.人物 + "',picture_org='" + info.机构 + "',picture_type='" + info.分类 + "',picture_marker='" + info.标引人 + "',picture_marktime='" + info.标引时间 + "',picture_width='" + info.图片宽 + "',picture_height='" + info.图片高 + "',picture_hr='" + info.水平分辨率 + "',picture_vr='" + info.垂直分辨率 + "',picture_bit='" + info.位深度 + "',picture_gps='" + info.gps信息 + "',picture_size='" + info.图片大小 + "',picture_format='" + info.图片格式 + "' where id='" + dt.Rows[0]["id"].ToString() + "';";
                int res = mysql.DBUpdateInsert(sql);
                if (res != 1)
                {
                    return "写入数据库失败";
                }
            }
            else
            {
                return "数据库数据有误，存在多条相同的记录";
            }
            return string.Empty;
        }
        /// <summary>
        /// 加载配置文件
        /// </summary>
        private void SetComboArray()
        {
            string excelPath = @"配置文件.xls";
            if (!File.Exists(excelPath))
            {
                throw new Exception("未找到配置文件");
            }
            DataTable dt = ExcelHandler.ReadExcelToDataTable(excelPath);
            if ((from DataColumn column in dt.Columns where column.ColumnName.Equals("分类") select column).ToArray().Length != 1)
            {
                throw new Exception("配置文件不符合要求");
            }
            if ((from DataColumn column in dt.Columns where column.ColumnName.Equals("人物") select column).ToArray().Length != 1)
            {
                throw new Exception("配置文件不符合要求");
            }
            if ((from DataColumn column in dt.Columns where column.ColumnName.Equals("机构") select column).ToArray().Length != 1)
            {
                throw new Exception("配置文件不符合要求");
            }

            List<string> typeList = new List<string>();
            List<string> personList = new List<string>();
            List<string> orgList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i]["分类"].ToString()))
                {
                    typeList.Add(dt.Rows[i]["分类"].ToString());
                }
                if (!string.IsNullOrEmpty(dt.Rows[i]["人物"].ToString()))
                {
                    personList.Add(dt.Rows[i]["人物"].ToString());
                }
                if (!string.IsNullOrEmpty(dt.Rows[i]["机构"].ToString()))
                {
                    orgList.Add(dt.Rows[i]["机构"].ToString());
                }
            }
            TypeConverter.Array = typeList.ToArray();
            PersonConverter.Array = personList.ToArray();
            OrgConverter.Array = orgList.ToArray();
        }

        /// <summary>
        /// 获取图片中的GPS坐标点
        /// </summary>
        /// <param name="p_图片路径">图片路径</param>
        /// <returns>返回坐标【纬度+经度】用"+"分割 取数组中第0和1个位置的值</returns>
        public String fnGPS坐标(String p_图片路径)
        {
            String s_GPS坐标 = "";
            //载入图片   
            Image objImage = Image.FromFile(p_图片路径);
            //取得所有的属性(以PropertyId做排序)   
            var propertyItems = objImage.PropertyItems.OrderBy(x => x.Id);
            //暂定纬度N(北纬)   
            char chrGPSLatitudeRef = 'N';
            //暂定经度为E(东经)   
            char chrGPSLongitudeRef = 'E';
            foreach (PropertyItem objItem in propertyItems)
            {
                //只取Id范围为0x0000到0x001e
                if (objItem.Id >= 0x0000 && objItem.Id <= 0x001e)
                {
                    objItem.Id = 0x0002;
                    switch (objItem.Id)
                    {
                        case 0x0000:
                            var query = from tmpb in objItem.Value select tmpb.ToString();
                            string sreVersion = string.Join(".", query.ToArray());
                            break;
                        case 0x0001:
                            chrGPSLatitudeRef = BitConverter.ToChar(objItem.Value, 0);
                            break;
                        case 0x0002:
                            if (objItem.Value.Length == 24)
                            {
                                //degrees(将byte[0]~byte[3]转成uint, 除以byte[4]~byte[7]转成的uint)   
                                double d = BitConverter.ToUInt32(objItem.Value, 0) * 1.0d / BitConverter.ToUInt32(objItem.Value, 4);
                                //minutes(将byte[8]~byte[11]转成uint, 除以byte[12]~byte[15]转成的uint)   
                                double m = BitConverter.ToUInt32(objItem.Value, 8) * 1.0d / BitConverter.ToUInt32(objItem.Value, 12);
                                //seconds(将byte[16]~byte[19]转成uint, 除以byte[20]~byte[23]转成的uint)   
                                double s = BitConverter.ToUInt32(objItem.Value, 16) * 1.0d / BitConverter.ToUInt32(objItem.Value, 20);
                                //计算经纬度数值, 如果是南纬, 要乘上(-1)   
                                double dblGPSLatitude = (((s / 60 + m) / 60) + d) * (chrGPSLatitudeRef.Equals('N') ? 1 : -1);
                                string strLatitude = string.Format("{0:#} deg {1:#}' {2:#.00}\" {3}", d
                                                                    , m, s, chrGPSLatitudeRef);
                                //纬度+经度
                                s_GPS坐标 += dblGPSLatitude + "+";
                            }
                            break;
                        case 0x0003:
                            //透过BitConverter, 将Value转成Char('E' / 'W')   
                            //此值在后续的Longitude计算上会用到   
                            chrGPSLongitudeRef = BitConverter.ToChar(objItem.Value, 0);
                            break;
                        case 0x0004:
                            if (objItem.Value.Length == 24)
                            {
                                //degrees(将byte[0]~byte[3]转成uint, 除以byte[4]~byte[7]转成的uint)   
                                double d = BitConverter.ToUInt32(objItem.Value, 0) * 1.0d / BitConverter.ToUInt32(objItem.Value, 4);
                                //minutes(将byte[8]~byte[11]转成uint, 除以byte[12]~byte[15]转成的uint)   
                                double m = BitConverter.ToUInt32(objItem.Value, 8) * 1.0d / BitConverter.ToUInt32(objItem.Value, 12);
                                //seconds(将byte[16]~byte[19]转成uint, 除以byte[20]~byte[23]转成的uint)   
                                double s = BitConverter.ToUInt32(objItem.Value, 16) * 1.0d / BitConverter.ToUInt32(objItem.Value, 20);
                                //计算精度的数值, 如果是西经, 要乘上(-1)   
                                double dblGPSLongitude = (((s / 60 + m) / 60) + d) * (chrGPSLongitudeRef.Equals('E') ? 1 : -1);
                            }
                            break;
                        case 0x0005:
                            string strAltitude = BitConverter.ToBoolean(objItem.Value, 0) ? "0" : "1";
                            break;
                        case 0x0006:
                            if (objItem.Value.Length == 8)
                            {
                                //将byte[0]~byte[3]转成uint, 除以byte[4]~byte[7]转成的uint   
                                double dblAltitude = BitConverter.ToUInt32(objItem.Value, 0) * 1.0d / BitConverter.ToUInt32(objItem.Value, 4);
                            }
                            break;
                    }
                }
            }
            return s_GPS坐标;
        }

        private string[] _pathArray;
        private string _localDBname = "local.db";

    }

    [DefaultPropertyAttribute("标题")]
    public class BitInfo
    {
        [CategoryAttribute("图片信息")
        DataTableConvertAttribute("picture_name")]
        public string 文件名称 { get; set; }

        [ReadOnlyAttribute(true)
        CategoryAttribute("图片信息")
        DataTableConvertAttribute("picture_file")]
        public string 路径 { get; set; }

        [CategoryAttribute("图片信息")
            DescriptionAttribute("标题")
        DataTableConvertAttribute("picture_title")]
        public string 标题 { get; set; }

        [CategoryAttribute("图片信息")
        DataTableConvertAttribute("picture_time")]
        public string 时间 { get; set; }

        [CategoryAttribute("图片信息")
        DataTableConvertAttribute("picture_author")]
        public string 作者 { get; set; }

        [CategoryAttribute("图片信息")
        DataTableConvertAttribute("picture_level")]
        public string 评级 { get; set; }

        [CategoryAttribute("图片信息")
        DataTableConvertAttribute("picture_info")]
        public string 摘要 { get; set; }

        [CategoryAttribute("图片信息")
        DataTableConvertAttribute("picture_key")]
        public string 关键词 { get; set; }

        [TypeConverter(typeof(PersonConverter))
            CategoryAttribute("图片信息")
        DataTableConvertAttribute("picture_person")]
        public string 人物 { get; set; }

        [TypeConverter(typeof(OrgConverter))
            CategoryAttribute("图片信息")
        DataTableConvertAttribute("picture_org")]
        public string 机构 { get; set; }

        [TypeConverter(typeof(TypeConverter))
            CategoryAttribute("图片信息")
        DataTableConvertAttribute("picture_type")]
        public string 分类 { get; set; }

        [CategoryAttribute("图片信息")
        DataTableConvertAttribute("picture_marker")]
        public string 标引人 { get; set; }

        [CategoryAttribute("图片信息")
        DataTableConvertAttribute("picture_marktime")]
        public string 标引时间 { get; set; }


        [CategoryAttribute("图片数据")
            ReadOnlyAttribute(true)
        DataTableConvertAttribute("picture_width")]
        public string 图片宽 { get; set; }

        [CategoryAttribute("图片数据")
            ReadOnlyAttribute(true)
        DataTableConvertAttribute("picture_height")]
        public string 图片高 { get; set; }

        [CategoryAttribute("图片数据")
            ReadOnlyAttribute(true)
        DataTableConvertAttribute("picture_hr")]
        public string 水平分辨率 { get; set; }

        [CategoryAttribute("图片数据")
            ReadOnlyAttribute(true)
        DataTableConvertAttribute("picture_vr")]
        public string 垂直分辨率 { get; set; }

        [CategoryAttribute("图片数据")
            ReadOnlyAttribute(true)
        DataTableConvertAttribute("picture_bit")]
        public string 位深度 { get; set; }

        [CategoryAttribute("图片数据")
            ReadOnlyAttribute(true)
        DataTableConvertAttribute("picture_gps")]
        public string gps信息 { get; set; }

        [CategoryAttribute("图片数据")
            ReadOnlyAttribute(true)
        DataTableConvertAttribute("picture_size")]
        public string 图片大小 { get; set; }

        [CategoryAttribute("图片数据")
            ReadOnlyAttribute(true)
        DataTableConvertAttribute("picture_format")]
        public string 图片格式 { get; set; }

        public BitInfo()
        {
            this.gps信息 = string.Empty;
            this.人物 = string.Empty;
            this.位深度 = string.Empty;
            this.作者 = string.Empty;
            this.关键词 = string.Empty;
            this.分类 = string.Empty;
            this.图片大小 = string.Empty;
            this.图片宽 = string.Empty;
            this.图片格式 = string.Empty;
            this.图片高 = string.Empty;
            this.垂直分辨率 = string.Empty;
            this.摘要 = string.Empty;
            this.文件名称 = string.Empty;
            this.时间 = string.Empty;
            this.机构 = string.Empty;
            this.标引人 = string.Empty;
            this.标引时间 = string.Empty;
            this.标题 = string.Empty;
            this.水平分辨率 = string.Empty;
            this.评级 = string.Empty;
            this.路径 = string.Empty;
        }

        public BitInfo Clone()
        {
            BitInfo info = new BitInfo();
            info.gps信息 = this.gps信息;
            info.人物 = this.人物;
            info.位深度 = this.位深度;
            info.作者 = this.作者;
            info.关键词 = this.关键词;
            info.分类 = this.分类;
            info.图片大小 = this.图片大小;
            info.图片宽 = this.图片宽;
            info.图片格式 = this.图片格式;
            info.图片高 = this.图片高;
            info.垂直分辨率 = this.垂直分辨率;
            info.摘要 = this.摘要;
            info.文件名称 = this.文件名称;
            info.时间 = this.时间;
            info.机构 = this.机构;
            info.标引人 = this.标引人;
            info.标引时间 = this.标引时间;
            info.标题 = this.标题;
            info.水平分辨率 = this.水平分辨率;
            info.评级 = this.评级;
            info.路径 = this.路径;
            return info;
        }
    }

    public class TypeConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(Array);
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

        public static string[] Array;
    }
    public class PersonConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(Array);
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }
        public static string[] Array;
    }
    public class OrgConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(Array);
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }
        public static string[] Array;
    }
    public class DataTableConvert
    {
        /// <summary>
        /// Table转为类的数组
        /// </summary>
        /// <typeparam name="T">要转为的类型</typeparam>
        /// <param name="table">存储数据的Table</param>
        /// <returns></returns>
        public static T[] TableToT<T>(DataTable table) where T : new()
        {
            if (table == null || table.Rows.Count == 0)
            {
                return null;
            }
            T[] rearray = new T[table.Rows.Count];
            Type modelType = typeof(T);
            var attrType = typeof(DataTableConvertAttribute);
            var properties = typeof(T).GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                var attr = Attribute.GetCustomAttribute(prop, attrType);
                if (attr != null)
                {
                    string name = (attr as DataTableConvertAttribute).Name;
                    if (!table.Columns.Contains(name))
                    {
                        throw new Exception("不包含" + name + "列");
                    }
                }
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                rearray[i] = new T();
                foreach (PropertyInfo prop in properties)
                {
                    var attr = Attribute.GetCustomAttribute(prop, attrType);
                    if (attr != null)
                    {
                        string name = (attr as DataTableConvertAttribute).Name;
                        object value = table.Rows[i][name].ToString();
                        if (string.IsNullOrEmpty((string)value))
                        {
                            continue;
                        }
                        if (value.GetType() != prop.PropertyType)
                        {
                            try
                            {
                                value = Convert.ChangeType(value, prop.PropertyType);
                            }
                            catch
                            {
                                throw new Exception(name + "列的格式不正确");
                            }
                        }
                        object e = rearray[i];
                        prop.SetValue(e, value, null);
                        rearray[i] = (T)e;
                    }
                }
            }
            return rearray;
        }

        /// <summary>
        /// 将数组转为DataTable
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static DataTable TableToT(Array array)
        {
            if (array == null || array.Length == 0)
            {
                return null;
            }
            DataTable table = new DataTable();
            Type modelType = array.GetValue(0).GetType();
            var attrType = typeof(DataTableConvertAttribute);
            var properties = modelType.GetProperties();
            foreach (PropertyInfo prop in properties)                    ///Table加列
            {
                var attr = Attribute.GetCustomAttribute(prop, attrType);
                if (attr == null)
                {
                    continue;
                }
                string name = (attr as DataTableConvertAttribute).Name;
                if (table.Columns.Contains(name))
                {
                    throw new Exception("特性名：" + name + "在不同属性之间重复出现");
                }
                table.Columns.Add(name, prop.PropertyType);
            }
            foreach (var va in array)                               ///数组赋值Table
            {
                DataRow row = table.NewRow();
                foreach (PropertyInfo prop in properties)
                {
                    var attr = Attribute.GetCustomAttribute(prop, attrType);
                    if (attr == null)
                    {
                        continue;
                    }
                    object value = prop.GetValue(va, null);
                    string columname = (attr as DataTableConvertAttribute).Name;
                    row[columname] = value;
                    table.Rows.Add(row);
                }
            }
            return table;
        }
    }
    public class DataTableConvertAttribute : Attribute
    {
        public string Name { get; set; }
        public DataTableConvertAttribute(string name)
        {
            this.Name = name;
        }
    }
}
