using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSMrp
{
    public class SqlData
    {
        int gbWareHouse;
        public SqlData(int warehouse)
        {
            gbWareHouse = warehouse;
        }
        public string GetData(string DataType, int Action)
        {
            string G = "";
            switch (DataType)
            {

                case "USER":
                    G = 使用者(Action);
                    break;
                case "MIS":
                    G = MIS(Action);
                    break;
                case "儲位":
                    G = 儲位(Action);
                    break;
                case "命令":
                    G = 命令(Action);
                    break;
                case "異動":
                    G = 異動(Action);
                    break;
                case "庫位":
                    G = 庫位(Action);
                    break;
                case "報表":
                    G = 報表(Action);
                    break;
                case "machineinfor":
                    G = machineinfor(Action);
                    break;
                case "其他":
                    G = 其他(Action);
                    break;
                case "SQLofStore":
                    G = SQLofStore(Action);
                    break;
                case "ShelfLife":
                    G = ShelfLife(Action);
                    break;
                default:
                    G = "";
                    break;
            }
            return G;
        }

        private string MIS(int Action)
        {
            string S = "";
            switch (Action)
            {
                case 1:
                    //取尚未完成入庫的資料
                    S = "SELECT SEQ_NO, TRX_NO, ITEM_NO, QUANTITY FROM Bill WHERE (STATUS = \'0\') AND (QUANTITY > 0) order by t" +
                    "rx_no,item_no";
                    break;
                case 2:
                    //取尚未完成入庫的資料
                    S = "SELECT SEQ_NO, TRX_NO, ITEM_NO, QUANTITY FROM Bill WHERE (STATUS = \'0\') AND (QUANTITY < 0) order by t" +
                    "rx_no,item_no";
                    break;
                case 3:
                    //修改已經完成的動作
                    S = "UPDATE Bill SET STATUS = \'1\' WHERE (SEQ_NO = ?1)";
                    break;
            }
            if (S.Length > 0)
            {
                return S;
            }
            return S;
        }

        private string 儲位(int Action)
        {
            string S = "";
            switch (Action)
            {
                //Store==================================================================================
                case 1: //取最大儲位代號
                    S = "SELECT MAX(storeno) AS Expr1 From Store";
                    break;

                case 2: //新增儲位
                    S = "INSERT INTO Store(storeno, storetype, machineno, carry, pos, depth, width, height, enabled) VALUES (?1, ?2, ?3, ?4, ?5, ?6, ?7, ?8,1)";
                    break;

                case 3: //取儲位資訊 by carry
                    S = "SELECT storeno,pos,depth,width,height,enabled from store where carry=?1 and machineno=?2";
                    break;

                case 4: //修改儲位資訊
                    S = "update store set pos=?1,depth=?2,width=?3,height=?4,enabled=?5 where storeno=?6";
                    break;

                case 5: //查詢(BY StoreNo)Store
                    S = "SELECT * FROM Store WHERE storeno=?1";
                    break;

                case 6: //取消暫時指定儲位
                    S = "UPDATE Store SET Enabled = 1 WHERE (Enabled = 2) AND (warehouse=" + gbWareHouse + ")";
                    break;

                case 7: //取消暫時指定儲位(未驗收)
                    S = "UPDATE Store SET Enabled = 3 WHERE (Enabled = 2)";
                    break;

                case 8: //取消暫時指定儲位(selectStore取消)
                    S = "UPDATE Store SET Enabled = 1 WHERE (StoreNo = ?1)";
                    break;

                case 9: //修改Store
                    S = "UPDATE Store SET  MinDate='?1',Enabled = ?2 WHERE (StoreNo = ?3)";
                    break;

                case 10: //平置倉所有儲位
                    S = "SELECT StoreNo, StoreTypeDesc FROM Store WHERE (WareHouse =" + gbWareHouse + ") AND (StoreType = 1) ORDER BY StoreTypeDesc";
                    break;

                case 83: //查詢有幾台機器
                    S = "Select distinct MachineNo From Store Where WareHouse = " + gbWareHouse + " AND MachineNo > 0 ORDER BY MachineNo";
                    break;
                case 84: //查詢第幾台全部層數(only Carry)
                    S = "Select distinct Carry From Store Where WareHouse = " + gbWareHouse + " AND MachineNo = (?1) ORDER BY Carry";
                    break;
                case 85: //查詢第幾台全部層數
                    S = "Select * From Store Where WareHouse = " + gbWareHouse + " AND MachineNo = (?1) ORDER BY Carry";
                    break;
                case 86: //查詢第幾台第幾層數
                    S = "Select * From Store Where WareHouse = " + gbWareHouse + " AND MachineNo = (?1) AND Carry = (?2) ORDER BY Carry";
                    break;

                //StoreM==================================================================================
                case 11: //新增StoreM
                    S = "INSERT INTO StoreM(StoreNo, MID, MQty,indate) VALUES (?1, ?2, ?3,getdate())";
                    break;

                case 12: //修改StoreM(入庫用)
                    S = "UPDATE StoreM SET MQty = ?1,indate=getdate() WHERE (StoreNo = ?2) and (mid=?3)";
                    break;

                case 13: //刪除StoreM
                    S = "DELETE FROM StoreM WHERE (id = ?1)";
                    break;

                case 14: //查詢(全部)StoreM
                    S = "SELECT * FROM StoreM";
                    break;

                case 15: //查詢(BY StoreNo)StoreM
                    S = "SELECT * FROM StoreM WHERE StoreNo=?1";
                    break;

                case 16: //刪除StoreM(空儲位)
                    S = "DELETE FROM StoreM WHERE (id IN (SELECT StoreM.id FROM Store INNER JOIN StoreM ON Store.StoreNo = StoreM.StoreNo WHERE (StoreM.MQty = 0)))";
                    break;

                case 17: //修改StoreM(領料後，扣除數量)
                    S = "UPDATE StoreM SET MQty = MQty-?1,indate=getdate() WHERE (StoreNo = ?2) and (mid=?3)";
                    break;

                case 18: //轉倉用的
                    S = "UPDATE StoreM SET StoreNo = ?1 WHERE (id = ?2)";
                    break;

                case 19: //ic倉更新location,ov,備註,備註2，2004/11/29新增此功能 2018/11/18增加備註2，2020/3/20增加庫位，2020/5/12新增廠務編號、LotCode
                    S = "UPDATE MMain SET Location ='?1', OV = '?2', Mdesc = '?3', Mdesc2 = '?4', StoreHouse = '?6', VanderName = '?7', LotCode = '?8' WHERE (id = ?5)";
                    break;

                case 20: //成品倉更新location,包裝，2019/3/6 新增此功能
                    S = "UPDATE MMain SET Location ='?1', PackageNo = ?2 WHERE (id = ?5)";
                    break;

                //MMain==================================================================================
                case 21: //新增MMain
                    S = "INSERT INTO MMain(StoreHouse, Mno, Location, WorkOrderNo, PackageNo,Mdesc,ov,Mdesc2) VALUES ('?1', '?2', '?3', '?4', ?5,'?6','?7','?8')";
                    break;

                case 22: //修改MMain的說明(半成品倉用)
                    S = "UPDATE MMain SET Mdesc = '?1' WHERE (id = ?2)";
                    break;

                case 23: //刪除MMain(空儲位)
                    S = "DELETE FROM MMain WHERE id IN(SELECT MMain.id FROM MMain LEFT OUTER JOIN StoreM ON MMain.id = StoreM.MID WHERE (StoreM.id IS NULL))";
                    break;

                case 24: //查詢(全部)MMain
                    S = "SELECT * FROM MMain";
                    break;

                case 25: //查詢(BY Mno)MMain
                    S = "SELECT * FROM MMain WHERE Mno='?1' And Location='?2' AND StoreHouse = '?3'";
                    break;

                case 26: //查詢MMain的最大id
                    S = "SELECT MAX(id) AS Expr1 FROM MMain";
                    break;

                case 27: //修改MMain的批號(ic倉用)
                    S = "UPDATE MMain SET OV = '?1' WHERE (id = ?2)";
                    break;

                case 87: // 2020/5/12 新增廠編、Lot Code
                    S = "INSERT INTO MMain(StoreHouse, Mno, Location, WorkOrderNo, PackageNo,Mdesc,ov,Mdesc2, VanderName, LotCode) VALUES ('?1', '?2', '?3', '?4', ?5,'?6','?7','?8','?9','?01')";
                    break;

                //綜合(一)====================================================================================
                case 31: //[入庫用] 找現有的物料儲位(BY Mno)(成品倉)
                    S = "SELECT Store.*,StoreM.MQty AS MQty FROM Store INNER JOIN StoreM ON Store.StoreNo = StoreM.StoreNo INNER JOIN MMain ON StoreM.MID = MMain.id WHERE (store.warehouse=" + gbWareHouse + ") AND (Store.storetype=0) AND (MMain.Mno = '?1') AND (MMain.Location = '?2') AND (MMain.StoreHouse = '?3') AND (MMain.PackageNo = ?4) Order by store.mindate desc, StoreM.id DESC";
                    break;

                case 32: //[入庫用] 找空的儲位(一次放)由小到大(成品倉)
                    S = "SELECT Store.* FROM Store WHERE (NOT (StoreNo IN (SELECT DISTINCT StoreNo FROM StoreM))) AND (Enabled = 1) AND (Width >= ?1) AND (Height >= ?2) AND (warehouse=?3) AND (Store.carry not in(1,2,3)) ORDER BY Store.Width, Store.Height";
                    break;

                case 33: //[領料用] 算數量[成品倉] BY Mno、Location、StoreHouse庫位、PackageNo包裝
                    S = "SELECT SUM(StoreM.MQty) AS TotalMQty FROM StoreM INNER JOIN MMain ON StoreM.MID = MMain.id WHERE (MMain.Mno = '?1') AND (MMain.Location = '?2') AND (MMain.StoreHouse = '?3')"; // AND (MMain.PackageNo = ?4)
                    break;

                case 34: //[領料用] 算數量[成品倉] BY Mno、StoreHouse庫位、PackageNo包裝
                    S = "SELECT SUM(StoreM.MQty) AS TotalMQty FROM StoreM INNER JOIN MMain ON StoreM.MID = MMain.id WHERE (MMain.Mno = '?1') AND (MMain.StoreHouse = '?2')"; // AND (MMain.PackageNo = ?3)
                    break;

                case 35: //[領料用] 找出現有的物料[成品倉] BY Mno、Location、StoreHouse庫位、PackageNo包裝 (數量由少->多)
                    S = "SELECT Store.*, StoreM.MQty,StoreM.MID, MMain.Mno, MMain.Location FROM StoreM INNER JOIN MMain ON StoreM.MID = MMain.id INNER JOIN Store ON StoreM.StoreNo = Store.StoreNo WHERE (Store.WareHouse=" + gbWareHouse + ") AND (MMain.Mno = '?1') AND (MMain.Location = '?2') AND (MMain.StoreHouse = '?3') ORDER BY Store.Mindate";  //AND (MMain.PackageNo = ?4)
                    break;

                case 36: //[領料用] 找出現有的物料[成品倉] BY Mno、StoreHouse庫位、PackageNo包裝 (數量由少->多)
                    S = "SELECT Store.*, StoreM.MQty,StoreM.MID, MMain.Mno, MMain.Location FROM StoreM INNER JOIN MMain ON StoreM.MID = MMain.id INNER JOIN Store ON StoreM.StoreNo = Store.StoreNo WHERE (Store.WareHouse=" + gbWareHouse + ") AND (MMain.Mno = '?1')  AND (MMain.StoreHouse = '?2')  ORDER BY Store.Mindate"; //AND (MMain.PackageNo = ?3)
                    break;

                case 37: //[領料用] 算數量[半成品倉] BY Mno、StoreHouse庫位、PackageNo包裝、WorkOrderNo工單單號
                    S = "SELECT SUM(StoreM.MQty) AS TotalMQty FROM StoreM INNER JOIN MMain ON StoreM.MID = MMain.id WHERE (MMain.Mno = '?1') AND (MMain.StoreHouse = '?2')  AND (MMain.WorkOrderNo = '?4')"; //AND (MMain.PackageNo = ?3)
                    break;

                case 38: //[領料用] 找出現有的物料[半成品倉] BY Mno、StoreHouse庫位、PackageNo包裝、WorkOrderNo工單單號 (數量由少->多);
                    S = "SELECT Store.*, StoreM.MQty, StoreM.MID, MMain.* FROM StoreM INNER JOIN MMain ON StoreM.MID = MMain.id INNER JOIN Store ON StoreM.StoreNo = Store.StoreNo WHERE (Store.WareHouse=" + gbWareHouse + ") AND (MMain.Mno = '?1')  AND (MMain.StoreHouse = '?2')  AND (MMain.WorkOrderNo = '?4') ORDER BY StoreM.MQty"; //AND (MMain.PackageNo = ?3)
                    break;
                case 39: //[入庫用] 找空的儲位(分批放)由大到小(半成品倉)
                    S = "SELECT Store.* FROM Store WHERE (NOT (StoreNo IN (SELECT DISTINCT StoreNo FROM StoreM))) AND (Enabled = 1)  AND (WareHouse=?1) ORDER BY Store.Width desc, Store.Height desc";
                    break;

                case 40: //[入庫用] 找現有的物料儲位(BY Mno)(半成品倉)
                    S = "SELECT Store.*,StoreM.MQty AS MQty FROM Store INNER JOIN StoreM ON Store.StoreNo = StoreM.StoreNo INNER JOIN MMain ON StoreM.MID = MMain.id WHERE (store.warehouse=" + gbWareHouse + ") AND (Store.storetype=0) AND (MMain.Mno = '?1') AND (MMain.WorkOrderNo = '?2') AND (MMain.StoreHouse = '?3') AND (MMain.PackageNo = ?4)";
                    break;

                case 41: //[入庫用] 找空的儲位(一次放)由小到大(半成品倉)
                    S = "SELECT Store.* FROM Store WHERE (NOT (StoreNo IN (SELECT DISTINCT StoreNo FROM StoreM))) AND (Enabled = 1) AND (Width >= ?1) AND (Height >= ?2) AND (warehouse=?3) ORDER BY Store.Width, Store.Height";
                    break;

                case 42: //[入庫用] 找空的儲位(分批放)由大到小(成品倉)
                    S = "SELECT Store.* FROM Store WHERE (NOT (StoreNo IN (SELECT DISTINCT StoreNo FROM StoreM))) AND (Enabled = 1)  AND (warehouse=?1) AND (Store.carry not in(1,2,3)) ORDER BY Store.Width desc, Store.Height desc";
                    break;

                case 43: //[入庫用] 找出每層空儲位的數量(合適的)儲位數量由小排到大
                    S = "SELECT MachineNo, Carry, COUNT(Carry) AS StoreCounts FROM view_EnptyStore WHERE (WareHouse=?1) GROUP BY MachineNo, Carry HAVING (COUNT(Carry) >= ?2) ORDER BY COUNT(Carry)";
                    break;

                case 44: //[入庫用] 找出每層空儲位的數量(沒有合適的)儲位數量由大排到小
                    S = "SELECT MachineNo, Carry, COUNT(Carry) AS StoreCounts FROM view_EnptyStore WHERE (WareHouse=?1) GROUP BY MachineNo, Carry HAVING (COUNT(Carry) >= 2) ORDER BY COUNT(Carry) DESC";
                    break;

                case 45: //[入庫用] 找出每層空儲位(合適的數量)
                    S = "SELECT * FROM view_EnptyStore WHERE (MachineNo = ?1) AND (Carry = ?2) AND (WareHouse=" + gbWareHouse + ") ORDER BY Carry";
                    break;

                //綜合(二)====================================================================================
                case 51: //查詢存放位置 by料號
                    S = "SELECT Store.MachineNo, Store.Carry, Store.Pos,Store.Depth, StoreM.MQty, MMain.StoreHouse, MMain.Mno, MMain.Location, MMain.WorkOrderNo, MMain.Mdesc,MMain.id as MMainID, Package.PackageDesc FROM Store INNER JOIN StoreM ON Store.StoreNo = StoreM.StoreNo INNER JOIN MMain ON StoreM.MID = MMain.id INNER JOIN Package ON MMain.PackageNo = Package.id WHERE (MMain.Mno ='?1') AND (store.warehouse=" + gbWareHouse + ") ORDER BY Store.MachineNo, Store.Carry, Store.Pos";
                    break;

                case 52: //查詢儲位的明細(by 機器、層、X、Y)
                    S = "SELECT StoreM.StoreNo,StoreM.MQty,StoreM.Mid, MMain.StoreHouse, MMain.Mno, MMain.Location, MMain.WorkOrderNo, MMain.PackageNo,MMain.OV, MMain.Mdesc, UPCdata.FinishNo, Package.PackageDesc, Package.id FROM Store INNER JOIN StoreM ON Store.StoreNo = StoreM.StoreNo INNER JOIN MMain ON StoreM.MID = MMain.id INNER JOIN Package ON MMain.PackageNo = Package.id LEFT OUTER JOIN UPCdata ON MMain.Mno = UPCdata.UPC WHERE (Store.MachineNo = ?1) AND (Store.Carry = ?2) AND (Store.Pos = ?3) AND (Store.Depth = ?4) AND (Store.WareHouse = ?5)";
                    break;

                case 53: //查詢存放位置 by工單
                    S = "SELECT Store.MachineNo, Store.Carry, Store.Pos,Store.Depth, StoreM.MQty, MMain.StoreHouse, MMain.Mno, MMain.Location, MMain.WorkOrderNo, MMain.Mdesc,MMain.id as MMainID, Package.PackageDesc FROM Store INNER JOIN StoreM ON Store.StoreNo = StoreM.StoreNo INNER JOIN MMain ON StoreM.MID = MMain.id INNER JOIN Package ON MMain.PackageNo = Package.id WHERE (MMain.WorkOrderNo = '?1') AND (store.warehouse=" + gbWareHouse + ") ORDER BY Store.MachineNo, Store.Carry, Store.Pos";
                    break;

                case 54: //查詢存放位置 by料號、工單
                    S = "SELECT Store.MachineNo, Store.Carry, Store.Pos,Store.Depth, StoreM.MQty, MMain.StoreHouse, MMain.Mno, MMain.Location, MMain.WorkOrderNo, MMain.Mdesc,MMain.id as MMainID, Package.PackageDesc FROM Store INNER JOIN StoreM ON Store.StoreNo = StoreM.StoreNo INNER JOIN MMain ON StoreM.MID = MMain.id INNER JOIN Package ON MMain.PackageNo = Package.id WHERE (MMain.Mno = '?1') and (MMain.WorkOrderNo = '?2') AND (store.warehouse=" + gbWareHouse + ") ORDER BY Store.MachineNo, Store.Carry, Store.Pos";
                    break;

                case 55: //HTML的儲位專用(by MachineNo)
                    S = "SELECT Store.*, StoreM.MQty, MMain.Mno, UPCdata.FinishNo, MMain.Location, MMain.WorkOrderNo, MMain.PackageNo, MMain.Mdesc, MMain.StoreHouse, Package.Width AS P_Width, Package.Height AS P_Height, Package.MaxQty AS P_MaxQty FROM MMain INNER JOIN StoreM ON MMain.id = StoreM.MID INNER JOIN Package ON MMain.PackageNo = Package.id RIGHT OUTER JOIN Store ON StoreM.StoreNo = Store.StoreNo LEFT OUTER JOIN UPCdata ON MMain.Mno = UPCdata.UPC WHERE (Store.MachineNo=?1) AND (Store.WareHouse=" + gbWareHouse + ") ORDER BY Store.MachineNo, Store.Carry, Store.Depth, Store.Pos";
                    break;

                case 56: //查詢存放位置 by料號(平置倉用) - 2020/5/12 新增廠編、Lotcode、Overdue、remaining
                    S = "SELECT Store.StoreNo, Store.StoreType, Store.MachineNo, Store.Carry, Store.Pos, Store.Depth, Store.StoreTypeDesc, StoreM.MID, StoreM.MQty, StoreM.InDate, MMain.Mno, MMain.StoreHouse, MMain.Location, MMain.OV, MMain.Mdesc, MMain.Mdesc2 ,MMain.VanderName, MMain.LotCode, MMain.Overdue, MMain.remaining FROM Store INNER JOIN StoreM ON Store.StoreNo = StoreM.StoreNo INNER JOIN MMain ON StoreM.MID = MMain.id WHERE (?1) AND (Store.WareHouse =" + gbWareHouse + ") ORDER BY Store.MachineNo, Store.Carry, Store.Pos";
                    break;

                case 57: //[ic倉]查詢儲位的明細(by 機器、層、X、Y)
                    S = "SELECT StoreM.id AS storemid, StoreM.MQty, StoreM.MID, MMain.StoreHouse, MMain.Mno, MMain.Location, MMain.WorkOrderNo, MMain.PackageNo, MMain.OV, MMain.Mdesc, MMain.Mdesc2, Package.PackageDesc, Store.StoreNo FROM Package RIGHT OUTER JOIN Store ON Package.id = Store.MTypeID LEFT OUTER JOIN MMain INNER JOIN StoreM ON MMain.id = StoreM.MID ON Store.StoreNo = StoreM.StoreNo  WHERE (Store.MachineNo = ?1) AND (Store.Carry = ?2) AND (Store.Pos = ?3) AND (Store.Depth = ?4) AND (Store.WareHouse = 3)";
                    break;

                case 58:    //[ic倉]查詢儲位的明細 - 2020/5/12 新增廠務編號、Lot Code
                    S = "SELECT StoreM.id AS storemid, StoreM.MQty, StoreM.MID, MMain.StoreHouse, MMain.Mno, MMain.Location, MMain.WorkOrderNo, MMain.PackageNo, MMain.OV, MMain.Mdesc, MMain.Mdesc2, Package.PackageDesc, Store.StoreNo, MMain.VanderName, MMain.LotCode FROM Package INNER JOIN Store ON Package.id = Store.MTypeID INNER JOIN MMain INNER JOIN StoreM ON MMain.id = StoreM.MID ON Store.StoreNo = StoreM.StoreNo WHERE (Store.MachineNo = ?1) AND (Store.Carry = ?2) AND (Store.Pos = ?3) AND (Store.Depth = ?4) AND (Store.WareHouse = 3)";
                    break;

                case 59: //HTML的儲位專用(by MachineNo)(IC倉專用)
                    S = "SELECT Store.*, StoreM.MQty, MMain.Mno, MMain.Location, MMain.ov, MMain.PackageNo, MMain.Mdesc, MMain.StoreHouse, StoreM.InDate AS Expr1 FROM MMain INNER JOIN StoreM ON MMain.id = StoreM.MID RIGHT OUTER JOIN Store ON StoreM.StoreNo = Store.StoreNo WHERE (Store.MachineNo = ?1) AND (Store.WareHouse = 3) ORDER BY Store.MachineNo, Store.Carry, Store.Depth, Store.Pos";
                    break;

                //綜合(一)====================================================================================
                case 61: //[入庫用] 找空的儲位，給包裝A、B用，只有1、2、3的Carry
                    S = "SELECT Store.* FROM Store WHERE (NOT (StoreNo IN (SELECT DISTINCT StoreNo FROM StoreM))) AND (Enabled = 1)  AND warehouse=" + gbWareHouse + " AND (MachineNo=?1) AND Store.carry in(1,2,3) ORDER BY Store.Width desc, Store.Height desc";
                    break;

                //ic倉用的 ====================================================================================
                case 71: //[平置倉] 2020/5/12 新增2020/5/12 新增廠務編號、Lotcode
                    S = "SELECT Store.StoreNo,storem.id as storemid, StoreM.MID, StoreM.MQty, MMain.* FROM Store INNER JOIN StoreM ON Store.StoreNo = StoreM.StoreNo INNER JOIN MMain ON StoreM.MID = MMain.id WHERE (Store.StoreNo = ?1)";
                    break;

                case 72: //[入庫用] 找空的儲位
                    S = "SELECT Store.* FROM Store WHERE (WareHouse = 3) AND (StoreType = 0) AND (NOT (StoreNo IN (SELECT DISTINCT StoreNo FROM StoreM))) AND (Enabled = 1) AND (MTypeID = ?1) AND (MachineNo in (?2))";
                    break;

                case 73: //[領料用] by 料號、location、ov
                    S = "SELECT Store.*, StoreM.MID, StoreM.MQty, MMain.Mno, MMain.StoreHouse, MMain.Location, MMain.WorkOrderNo, MMain.OV, MMain.Mdesc, MMain.PackageNo, StoreM.InDate FROM MMain INNER JOIN StoreM ON MMain.id = StoreM.MID INNER JOIN Store ON StoreM.StoreNo = Store.StoreNo WHERE (Store.WareHouse =" + gbWareHouse + "  ) AND (MMain.Mno = '?1') AND (MMain.StoreHouse = '?2') ?3 ORDER BY StoreM.InDate";
                    break;

                case 74: //[領料用] by 料號、location、ov -- 2020/5/12 新增廠務編號、Lotcode
                    S = "SELECT SUM(StoreM.MQty) AS TotalMQty FROM MMain INNER JOIN StoreM ON MMain.id = StoreM.MID INNER JOIN Store ON StoreM.StoreNo = Store.StoreNo WHERE (Store.WareHouse =" + gbWareHouse + ") AND (MMain.Mno = '?1') AND (MMain.StoreHouse = '?2') ?3";
                    break;

                case 75: //[ic]查詢儲位的分類
                    S = "SELECT Store.WareHouse, Store.MachineNo, Store.Carry, Package.PackageDesc,Store.MTypeID FROM Store LEFT OUTER JOIN Package ON Store.MTypeID = Package.id GROUP BY Store.WareHouse, Store.MachineNo, Store.Carry, Package.PackageDesc,Store.MTypeID, Store.StoreType HAVING (Store.WareHouse = ?1) AND (Store.StoreType = 0) order by store.machineno,store.carry";
                    break;

                case 76: //[ic]修改儲位的分類
                    S = "UPDATE Store SET MTypeID = ?1 WHERE (WareHouse = ?2) AND (MachineNo = ?3) AND (Carry = ?4) AND (StoreType = 0)";
                    break;

                case 77: //刪除store
                    S = "DELETE FROM Store WHERE (WareHouse = 3) AND (MachineNo = ?1) AND (Carry = ?2)";
                    break;

                case 78: //檢查是否尚有庫存(by机器、層)
                    S = "SELECT Store.StoreNo, StoreM.MID FROM Store INNER JOIN StoreM ON Store.StoreNo = StoreM.StoreNo WHERE (Store.WareHouse = 3) AND (Store.MachineNo = ?1) AND (Store.Carry = ?2)";
                    break;

                case 79: //
                    S = "SELECT Width FROM Package WHERE (id = ?1)";
                    break;

                case 80: //修改MMain的工單(半成品倉用)
                    S = "UPDATE MMain SET WorkOrderNo = '?1' WHERE (id = ?2)";
                    break;

                case 81: //找STORENO
                    S = "SELECT Store.Storeno FROM Store INNER JOIN StoreM ON Store.StoreNo = StoreM.StoreNo INNER JOIN MMain ON StoreM.MID = MMain.id INNER JOIN Package ON MMain.PackageNo = Package.id WHERE (MMain.Mno = '?1') and (MMain.WorkOrderNo = '?2') AND (store.warehouse=" + gbWareHouse + ") ";
                    break;

                case 82: //[領料用] -- 2020/5/12 新增廠務編號、Lotcode、Overdue、remaining
                    S = "SELECT Store.*, StoreM.MID, StoreM.MQty, MMain.Mno, MMain.StoreHouse, MMain.Location, MMain.WorkOrderNo, MMain.OV, MMain.Mdesc,MMain.Mdesc2, MMain.PackageNo, StoreM.InDate, MMain.VanderName, MMain.LotCode, MMain.Overdue, MMain.remaining FROM MMain INNER JOIN StoreM ON MMain.id = StoreM.MID INNER JOIN Store ON StoreM.StoreNo = Store.StoreNo WHERE (Store.WareHouse =" + gbWareHouse + "  ) AND (MMain.StoreHouse = '?2') ?3 ORDER BY StoreM.InDate";
                    break;
            }

            if (S.Length > 0)
            { return S; }

            return S;
        }

        private string 異動(int Action)
        {
            string S = "";
            switch (Action)
            {
                case 1: //取異動記錄byPID
                    S = "SELECT TransNo, mno, StoreNo, Transdate, TransUser, TransType, TransQty, TransMemo, TransDevice, Complete, PID FROM Mtrans WHERE (PID = ?1)";
                    break;

                case 2: //修改異動記錄完成
                    S = "UPDATE Mtrans SET Complete = 1 WHERE (PID = ?1)";
                    break;

                case 3: //取最大異動代號
                    S = "SELECT MAX(PID) AS MaxPID FROM Mtrans";
                    break;

                case 4: //新增異動記錄(入出、領料) -- 2020/5/12 新增廠務編號、Lotcode
                    S = "INSERT INTO Mtrans(TransNo, mno, StoreNo, Transdate, TransUser, TransType, TransQty, TransMemo, TransDevice, Complete, PID,location,StoreHouse,ov,warehouse,Mdesc2,Mdesc,VanderName,LotCode) ";
                    S = S + " VALUES ('?1', '?2', ?3, '?4', ?5, ?6, ?7, '?8', ?9, 0, ?01,'?02','?03','?04'," + gbWareHouse + ",'?05','?06','?07','?08')";
                    break;

                case 13: //新增異動記錄(入出、領料) - 2020/5/12 新增廠編、LotCode
                    S = "INSERT INTO Mtrans(TransNo, mno, StoreNo, Transdate, TransUser, TransType, TransQty, TransMemo, TransDevice, Complete, PID,location,StoreHouse,ov,warehouse,Mdesc2,Mdesc,VanderName,LotCode) ";
                    S = S + " VALUES ('?1', '?2', ?3, '?4', ?5, ?6, ?7, '?8', ?9, 0, ?01,'?02','?03','?04'," + gbWareHouse + ",'?05','?06','?07','?08')";
                    break;

                case 5: //取未完成異動單
                    S = "SELECT * FROM Mtrans WHERE (Complete = 0)";
                    break;

                case 6: //取未完成異動單
                    S = "SELECT Top 200 Mtrans.*, Store.Carry, Store.Pos , Store.Depth, UPCdata.FinishNo FROM Mtrans INNER JOIN Store ON Mtrans.StoreNo = Store.StoreNo LEFT OUTER JOIN UPCdata ON Mtrans.mno = UPCdata.UPC WHERE (Mtrans.Complete = 0) AND (Mtrans.WareHouse=?1) AND (Store.StoreType = 0) order by Mtrans.id desc";
                    break;

                case 7: //新增異動記錄(盤點) -- 2020/5/12新增廠務編號、LotCode
                    S = "INSERT INTO Mtrans(TransNo, mno, StoreNo, Transdate, TransUser, TransType, TransQty, TransMemo, TransDevice, Complete, PID,location,StoreHouse,ov,warehouse,Mdesc2,Mdesc,VanderName,LotCode) ";
                    S = S + " VALUES ('?1', '?2', ?3, '?4', ?5, ?6, ?7, '?8', ?9, 1, ?01,'?02','?03','?04'," + gbWareHouse + ",'?05','?06','?07','?08')";
                    break;

                case 8: //錯誤資料維護
                    S = "UPDATE Mtrans SET Complete = 1 WHERE WareHouse=" + gbWareHouse;
                    break;

                case 9: //新增異動記錄(入出、領料)(平置倉用的) -- 2020/5/12 新增廠編、Lotcode
                    S = "INSERT INTO Mtrans(TransNo, mno, StoreNo, Transdate, TransUser, TransType, TransQty, TransMemo, TransDevice, Complete, PID,location,StoreHouse,ov,warehouse,Mdesc2,Mdesc,VanderName,LotCode) ";
                    S = S + " VALUES ('?1', '?2', ?3, '?4', ?5, ?6, ?7, '?8', ?9, 0, ?01,'?02','?03','?04'," + gbWareHouse + ",'?05','?06','?07','?08')";
                    break;
                case 14: //新增異動記錄(入出、領料)(平置倉用的) - 2020/5/12 新增廠編、Lotcode
                    S = "INSERT INTO Mtrans(TransNo, mno, StoreNo, Transdate, TransUser, TransType, TransQty, TransMemo, TransDevice, Complete, PID,location,StoreHouse,ov,warehouse,Mdesc2,Mdesc,VanderName,LotCode) ";
                    S = S + " VALUES ('?1', '?2', ?3, '?4', ?5, ?6, ?7, '?8', ?9, 0, ?01,'?02','?03','?04'," + gbWareHouse + ",'?05','?06','?07','?08')";
                    break;

                case 10: //[ic倉]之自動倉未完成異動單資料
                    S = "SELECT Top 200 Mtrans.*, Store.Carry, Store.Pos, Store.Depth, Store.StoreTypeDesc FROM Mtrans INNER JOIN Store ON Mtrans.StoreNo = Store.StoreNo WHERE (Mtrans.Complete = 0) AND (Mtrans.WareHouse = 3) AND  (Store.StoreType = 0) AND (Store.MachineNo in (?1)) ORDER BY Mtrans.id desc";
                    break;

                case 11: //[ic倉]之平置倉未完成異動單資料
                    S = "SELECT Top 1000 Mtrans.*, Store.Carry, Store.Pos, Store.Depth, Store.StoreTypeDesc FROM Mtrans INNER JOIN Store ON Mtrans.StoreNo = Store.StoreNo WHERE (Mtrans.Complete = 0) AND (Mtrans.WareHouse = 3) AND (Store.StoreType = 1) ORDER BY Mtrans.id desc";
                    break;

                case 12: //修改平置倉異動資料=1(完成)
                    S = "UPDATE Mtrans SET Complete = 1 WHERE (StoreNo IN (SELECT StoreNo FROM Store WHERE (StoreType = 1)))";
                    break;
            }

            if (S.Length > 0)
            {
                return S;
            }

            return S;
        }

        private string 庫位(int Action)
        {
            string S = "";

            switch (Action)
            {
                case 1: //取最大庫位號
                    S = "SELECT MAX(id) AS Expr1 FROM flatST";
                    break;

                case 2: //新增庫位
                    S = "INSERT INTO flatST(StoreDesc) VALUES ('?1')";
                    break;

                case 3: //取平置倉庫位
                    S = "SELECT id, StoreDesc FROM flatST";
                    break;

                case 4: //修改庫位說明
                    S = "UPDATE flatST SET StoreDesc = '?1' WHERE (id = ?2)";
                    break;

                case 5: //刪除庫位
                    S = "DELETE FROM flatST WHERE (id = ?1)";
                    break;

                case 6:
                    S = "SELECT id,storedesc From FlatST WHERE (LEFT(storedesc, 1) ='?1')";
                    break;

                case 7: //找盤空位(22)
                    S = "SELECT COUNT(*) AS StoreCount From Store WHERE Store.WareHouse = 3 AND Store.MachineNo in (?2) AND Store.MTypeID = 22 AND Store.StoreNo NOT IN (SELECT StoreNo FROM StoreM)";
                    break;

                case 8: //找捲空位(23)
                    S = "SELECT COUNT(*) AS StoreCount From Store WHERE Store.WareHouse = 3 AND Store.MachineNo in (?2) AND Store.MTypeID = 23 AND Store.StoreNo NOT IN (SELECT StoreNo FROM StoreM)";
                    break;
                case 9: //總儲位
                    S = "SELECT COUNT(*) AS StoreCount From Store WHERE Store.WareHouse = 3 AND Store.MachineNo in (?2)";
                    break;
            }

            if (S.Length > 0)
            {
                return S;
            }
            return S;
        }

        private string 報表(int Action)
        {
            string S = "";

            switch (Action)
            {
                case 1: //新增入出庫報表記錄
                    S = "INSERT INTO IO(日期, La_No, Bill_No, Lot_No, 片數, DIE數, 架位) VALUES (: '?1: ', : '?2: ', : '?3: ', : '?4: ', ?5, ?6, : '?7: ')";
                    break;

                case 2:
                    S = "INSERT INTO IOTotal(日期, La_No, 入庫W, 入庫D, 出庫W, 出庫D, 結餘W, 結餘D, 異動儲位) VALUES (: '?1: ', : '?2: ', ?3, ?4, ?5, ?6, ?7, ?8, : '?9: ')";
                    break;

                case 3: //取結餘
                    S = "SELECT SramLog.PRODUCT_NO, COUNT(SramLog.LOT) AS Expr3, SUM(SramLog.WAFER_QTY) AS Expr1, SUM(SramLog.DIE_QTY) AS Expr2 FROM Store INNER JOIN SramLog ON Store.LotID = SramLog.id GROUP BY SramLog.PRODUCT_NO HAVING (SramLog.PRODUCT_NO IN (SELECT SramLog.PRODUCT_NO FROM Mtrans INNER JOIN SramLog ON Mtrans.LotID = SramLog.id GROUP BY SramLog.PRODUCT_NO))";
                    break;

                case 4:
                    S = "INSERT INTO Store(批號, LA, 設備, 層, 橫, 縱, W, D) VALUES (: '?1: ', : '?2: ', : '?3: ', : '?4: ', ?5, ?6, ?7,?8)";
                    break;
            }

            if (S.Length > 0)
            {
                return S;
            }
            return S;
        }

        private string machineinfor(int Action)
        {
            string S = "";

            switch (Action)
            {
                case 1: //查詢設備(All)
                    S = "SELECT * FROM Machine WHERE warehouse=?1";
                    break;

                case 2: //查詢設備(BY MachineNo)
                    S = "SELECT * FROM Machine WHERE (machineno = ?1) and (warehouse=?2)";
                    break;

                case 3: //新增設備
                    S = "INSERT INTO Machine(id,machineno,machinedesc,maxcarry,devicenum,machinetype,MaxX,MaxY,WareHouse) VALUES (?1,?2,'?3',?4,?5,?6,?7,?8,?9)";
                    break;

                case 4: //修改設備
                    S = "UPDATE Machine SET machineno=?2,machinedesc ='?3',maxcarry=?4,devicenum=?5,machinetype=?6,MaxX=?7,MaxY=?8,WareHouse=?9 WHERE (id = ?1)";
                    break;

                case 5: //刪除設備
                    S = "DELETE FROM Machine WHERE (id = ?1)";
                    break;

                case 6: //查詢設備(All)
                    S = "SELECT * FROM Machine ";
                    break;

                case 7: //查詢MaxID
                    S = "SELECT Max(ID) FROM Machine ";
                    break;
            }

            if (S.Length > 0)
            {
                return S;
            }
            return S;
        }

        private string 使用者(int Action)
        {
            string S = "";

            switch (Action)
            {
                case 1: //新增User
                    S = "INSERT INTO UserData (userid, username, userpassword, userpower,warehouse) VALUES ('?1', '?2', '?3', '?4'," + gbWareHouse + ")";
                    break;

                case 2: //修改User
                    S = "UPDATE UserData SET userid = '?1', username = '?2', userpassword = '?3', userpower = '?4' WHERE (id = ?5)";
                    break;

                case 3: //刪除User
                    S = "DELETE FROM UserData WHERE (id = ?1)";
                    break;

                case 4: //查詢User(全部)
                    S = "SELECT id, userid, username, userpassword, userpower FROM UserData WHERE WareHouse=" + gbWareHouse;
                    break;

                case 5: //查詢User(BY ID)
                    S = "SELECT id, userid, username, userpassword, userpower FROM UserData WHERE (id = ?1)";
                    break;

                case 6: //查詢User(BY UserID,Password)
                    S = "SELECT * FROM UserData WHERE (userid ='?1') AND (userpassword ='?2') AND WareHouse=" + gbWareHouse;
                    break;

                case 7: //修改密碼
                    S = "UPDATE UserData SET userpassword ='?1' WHERE (id = ?2)";
                    break;

                case 8: //查詢User(BY Password)
                    S = "SELECT id, userid, username, userpassword, userpower FROM UserData WHERE (userpassword = '?1')";
                    break;
            }

            if (S.Length > 0)
            {
                return S;
            }
            return S;
        }

        private string 命令(int Action)
        {
            string S = "";

            switch (Action)
            {
                case 1: //新增命令
                    S = "INSERT INTO Command(Command, PID) VALUES ('?1', ?2)";
                    break;

                case 2: //新增測試命令
                    S = "INSERT INTO TopLevel(Comamnd) VALUES ('?1')";
                    break;

                case 3: //取完成命令的ID
                    S = "SELECT PID FROM Trans";
                    break;

                case 4: //刪除完成命令的ID
                    S = "DELETE FROM Trans WHERE (PID = ?1)";
                    break;

                case 5: //刪除WaitTrans BY ID
                    S = "DELETE FROM WaitTrans WHERE (PID = ?1)";
                    break;

                //C3E1/E2Buffer=========================================================
                case 11:
                    S = "INSERT INTO C3E1Buffer(HostID, Tray, pos, depth, qty, msg1,msg2,msg3,msg4, PID,MachineNo) VALUES (?01, ?02, ?03, ?04, ?05,'?06','?07','?08','?09', ?10, ?11)";
                    break;

                case 12:
                    S = "INSERT INTO C3E2Buffer(HostID, Tray, pos, depth, qty, msg1,msg2,msg3,msg4, PID,MachineNo) VALUES (?01, ?02, ?03, ?04, ?05,'?06','?07','?08','?09', ?10, ?11)";
                    break;
            }

            if (S.Length > 0)
            {
                return S;
            }
            return S;
        }

        private string 其他(int Action)
        {
            string S = "";

            switch (Action)
            {
                //StoreHouse庫位================================================================================
                case 1: //新增庫位
                    S = "INSERT INTO StoreHouse(StoreHouse) VALUES ('?1')";
                    break;

                case 2: //修改庫位
                    S = "UPDATE StoreHouse SET StoreHouse = '?1' WHERE (id = ?2)";
                    break;

                case 3: //刪除庫位
                    S = "DELETE FROM StoreHouse WHERE (id = '?1')";
                    break;

                case 4: //查詢全部庫位
                    S = "SELECT * FROM StoreHouse ORDER BY StoreHouse";
                    break;

                case 5: //檢查庫位庫存
                    S = "SELECT Count(MMain.id) FROM MMain,StoreM WHERE MMain.id=StoreM.MID and MMain.StoreHouse='?1'";
                    break;

                //Package包裝===================================================================================
                case 11: //新增包裝
                    S = "INSERT INTO Package(MachineNo, PackageDesc, Width, Height, MaxQty) VALUES ('?1','?2','?3', '?4', '?5')";
                    break;

                case 12: //修改包裝
                    S = "UPDATE Package SET MachineNo='?1',PackageDesc ='?2', Width = '?3', Height = '?4', MaxQty = '?5' WHERE (id = '?6')";
                    break;

                case 13: //刪除包裝
                    S = "DELETE FROM Package WHERE (id = ?1)";
                    break;

                case 14: //查詢包裝(全部)
                    S = "SELECT * FROM Package WHERE (MachineNo = ?1) ORDER BY PackageDesc";
                    break;

                case 15: //查詢包裝(by id)
                    S = "SELECT * FROM Package WHERE (id = ?1)";
                    break;

                //UPCdata料號比對(成品料號<-->UPC code)=========================================================
                case 21: //新增比對資料
                    S = "INSERT INTO UPCdata(UPC, FinishNo) VALUES ('?1', '?2')";
                    break;

                case 22: //修改比對資料
                    S = "UPDATE UPCdata SET FinishNo = '?1' WHERE (UPC = '?2')";
                    break;

                case 23: //刪除tempUSA
                    S = "DELETE TempUSA";
                    break;

                case 24: //查詢比對資料(UPC->FinishNo)
                    S = "SELECT FinishNo FROM UPCdata WHERE (UPC = '?1')";
                    break;

                case 25: //查詢比對資料(FinishNo->UPC)
                    S = "SELECT UPC FROM UPCdata WHERE (FinishNo = '?1')";
                    break;

                case 26: //把空白去掉
                    S = "UPDATE UPCdata SET FinishNo = RTRIM(FinishNo), UPC = RTRIM(UPC)";
                    break;

                case 27: //USA的資料和ROC的資料比對轉入[TempUSA]
                    S = "INSERT INTO TempUSA(StoreHouse, Mno, Qty1, Qty2) SELECT view_DataROC.StoreHouse, view_DataROC.FinishNo, view_DataROC.MQty, USAdata.Qty FROM view_DataROC LEFT OUTER JOIN USAdata ON view_DataROC.StoreHouse = USAdata.StoreHouse AND view_DataROC.FinishNo = USAdata.FinishNo WHERE (USAdata.Location = N'')";
                    break;

                case 28: //修改[tempUSA]
                    S = "UPDATE TempUSA SET Qty3 =Qty1-Qty2";
                    break;

                case 29: //修改[tempUSA]
                    S = "UPDATE TempUSA SET Qty1 =0 WHERE Qty1 is null";
                    break;

                case 30: //修改[tempUSA]
                    S = "UPDATE TempUSA SET Qty2 =0 WHERE Qty2 is null";
                    break;

                case 40: //[成品倉之平置倉用]
                    S = "INSERT INTO TempUSA(StoreHouse, Mno, Qty1, Qty2) SELECT USAdata.StoreHouse, USAdata.FinishNo, view_DataROC.MQty, USAdata.Qty FROM view_DataROC RIGHT OUTER JOIN USAdata ON view_DataROC.StoreHouse = USAdata.StoreHouse AND view_DataROC.FinishNo = USAdata.FinishNo WHERE (USAdata.Location = N'') and ((USAdata.FinishNo LIKE N'C%') OR (USAdata.FinishNo LIKE N'D%') OR (USAdata.FinishNo LIKE N'F%') OR (USAdata.FinishNo LIKE N'K%') OR (USAdata.FinishNo LIKE N'M%') OR (USAdata.FinishNo LIKE N'P%') OR (USAdata.FinishNo LIKE N'S%'))";
                    break;

                case 39: //[成品倉之平置倉用]
                    S = "UPDATE TempUSA SET Qty3 =Qty2-Qty1";
                    break;

                case 38:
                    S = "SELECT TempUSA.StoreHouse, TempUSA.Mno, pStorePos.StorePos, TempUSA.Qty3 FROM pStorePos RIGHT OUTER JOIN TempUSA ON pStorePos.Mno = TempUSA.Mno ?1 ORDER BY pStorePos.StorePos, TempUSA.StoreHouse, TempUSA.Mno";
                    break;

                //log_code99 料號檢查===========================================================================
                case 31: //新增log資料
                    S = "INSERT INTO log_code99(code99,warehouse) VALUES ('?1'," + gbWareHouse + ")";
                    break;

                case 34: //查詢log資料
                    S = "SELECT * FROM log_code99 WHERE (code99 = '?1' and warehouse=" + gbWareHouse + ")";
                    break;

                //平置倉儲位對應表=============================================================================
                case 41:
                    S = "INSERT INTO pStorePos(Mno, StorePos) VALUES ('?1', '?2')";
                    break;

                case 42:
                    S = "UPDATE pStorePos SET StorePos = '?1' WHERE (Mno = '?2')";
                    break;

                case 44:
                    S = "SELECT StorePos FROM pStorePos WHERE (Mno = '?1')";
                    break;
            }

            if (S.Length > 0)
            {
                return S;
            }
            return S;
        }

        public string SQLofStore(int Action)
        {
            string S = "";

            switch (Action)
            {
                case 1: //取最大儲位代號
                    S = "SELECT MAX(storeno) AS Expr1 From Store";
                    break;

                case 2: //新增儲位
                    S = "INSERT INTO  StoreM (storeno, storetype, machineno, carry, pos, depth, width, height, enabled,WareHouse) VALUES (?1, ?2, ?3, ?4, ?5, ?6, ?7, ?8,1,?9)";
                    break;

                case 3: //取儲位資訊 by carry
                    S = "SELECT storeno,pos,depth,width,height,enabled from StoreM where carry=?1 and machineno=?2 and warehouse=?3";
                    break;

                case 4: //修改儲位資訊
                    S = "update Store set pos=?1,depth=?2,width=?3,height=?4,enabled=?5 where storeno=?6";
                    break;


                case 7: //新增平置倉
                    S = "INSERT INTO Store(StoreNo, StoreType, MachineNo, Carry, Pos, Depth, Width, Height, Enabled, WareHouse, StoreTypeDesc) VALUES (?1, 1, 0, 0, 0, 0, 0, 0, 1, ?2, '?3')";
                    break;

                case 8: //刪除平置倉
                    S = "Delete from Store where storeno =?1";
                    break;

                case 9:
                    S = "SELECT StoreNo, StoreTypeDesc FROM Store WHERE (WareHouse = ?1) AND (StoreType = 1) order by storetypedesc";
                    break;
                case 10: //修改平置倉名稱
                    S = "UPDATE Store SET StoreTypeDesc='?3' where storeno =?1";
                    break;
            }

            if (S.Length > 0)
            {
                return S;
            }

            return S;
        }

        public string ShelfLife(int Action)
        {
            string S = "";

            switch (Action)
            {
                case 1: //查詢ShelfLife設定
                    S = "SELECT * FROM MainShelfLife ORDER BY ShelfLife";
                    break;
                case 2: //查詢料號
                    S = "SELECT Count(MMain.id) FROM MMain,StoreM WHERE MMain.id=StoreM.MID AND MMain.Mno like '?1%'";
                    break;
                case 3: //刪除ShelfLife設定
                    S = "DELETE FROM MainShelfLife WHERE (PK = '?1')";
                    break;
                case 4: //新增ShelfLife設定
                    S = "INSERT INTO MainShelfLife(KTCPartNo,ShelfLife) VALUES ('?1','?2')";
                    break;
                case 5: //修改ShelfLife設定
                    S = "UPDATE MainShelfLife SET KTCPartNo = '?1',ShelfLife = '?2' WHERE (PK = ?3)";
                    break;
                case 6: //ShelfLife設定結合週數
                    S = "SELECT * FROM MainShelfLife,DateMW WHERE MainShelfLife.ShelfLife=DateMW.DateM ORDER BY ShelfLife";
                    break;
            }

            if (S.Length > 0)
            {
                return S;
            }
            return S;
        }
    }
}
