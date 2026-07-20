import { Fetch, setValue } from '@syncfusion/ej2-base';
import { DataManager, ODataV4Adaptor, Query, type DataOptions, type DataResult, type CrudOptions } from '@syncfusion/ej2-data';

export class CustomAdaptor extends ODataV4Adaptor {
    public processQuery(dm: DataManager, query: Query): Object {
        dm.dataSource.url = 'http://localhost:5092/odata/Orders'; // Replace <port> with your backend port (for example, 5001 or 7001).
        query.addParams('Syncfusion Pivot Table', 'true'); // Add the additional parameter.
        return super.processQuery(dm, query);
    }

    public beforeSend(dm: DataManager, request: Request, settings?: Fetch): void {
        // Replace the placeholder below with a real token (for example, `Bearer <token>`).
        request.headers.set('Authorization', `Bearer <token>`);
        super.beforeSend(dm, request, settings as Fetch);
    }

    public processResponse(data: DataResult, ds?: DataOptions, query?: Query, xhr?: Request, request?: Fetch, changes?: CrudOptions): Object {
        let i: number = 0;
        const original: DataResult = super.processResponse(data, ds, query, xhr, request, changes) as DataResult;
        // Adding serial number.
        if (original.result) {
            (original.result as Object[]).forEach((item: Object) => setValue('SNo', ++i, item));
        }
        return original;
    }
}