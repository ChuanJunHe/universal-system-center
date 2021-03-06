import { Injectable } from "@angular/core";
import { _HttpClient } from "@delon/theme";
import { HttpHeaders } from "@angular/common/http";
import { Page } from "@shared/model/page";


@Injectable()
/**
 * 系统管理-区域服务类
 */
export class AreaService {
    constructor(public http: _HttpClient) {

    }

    /**
     * 获取列表
     * @param page 
     */
    getList(page: Page) {
        return this.http.get(`api/Area/page?${page.formatToParams()}`);
    }


    /**
     * 删除
     * @param ids 
     */
    delete(ids) {
        return this.http.delete("api/Area/delete", { ids: ids });
    }



    /**
     * 详情
     * @param id 
     */
    getDetail(id) {
        return this.http.get('api/Area/' + id);
    }


    /**
     * 添加
     * @param parmas 
     */
    add(parmas) {
        return this.http.post('api/Area/add', '', parmas);
    }

    /**
     * 编辑
     * @param parmas 
     */
    edit(parmas) {
        return this.http.post('api/Area/edit', '', parmas);
    }


    /**
     * 获取待处理坐标的区域
     * @param page 
     */
    GetHandleCoordList(page: Page) {
        return this.http.get(`api/Area/GetHandleCoordList?${page.formatToParams()}`);
    }

    /**
     * 处理区域坐标
     * @param parmas 
     */
    HandleCoord(parmas) {
        let entityJson = JSON.stringify(parmas);
        let _params = 'dataJson=' + entityJson;
        let header = { headers: new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' }) };
        return this.http.post("api/Area/HandleCoord", _params, '', header);
    }

}
