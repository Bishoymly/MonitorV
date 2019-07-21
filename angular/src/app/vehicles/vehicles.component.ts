import { Component, Injector } from '@angular/core';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from 'shared/paged-listing-component-base';
import { VehiclesServiceProxy, PagedResultDtoOfVehicleDto, VehicleDto } from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';

class PagedVehiclesRequestDto extends PagedRequestDto {
    customer: string;
}

@Component({
    templateUrl: './vehicles.component.html',
    animations: [appModuleAnimation()],
    styles: [
        `
          mat-form-field {
            padding: 10px;
          }
        `
      ]
})
export class VehiclesComponent extends PagedListingComponentBase<VehicleDto> {
    
    vehicles: VehicleDto[] = [];
    filteredVehicles: VehicleDto[] = [];
    customers: string[] = [];
    customer = '';
    showOnline = '';

    constructor(
        injector: Injector,
        private _vehiclesService: VehiclesServiceProxy,
        private _dialog: MatDialog
    ) {
        super(injector);
    }

    ngOnInit(): void {
        super.ngOnInit();

        abp.event.on('abp.notifications.received', userNotification => {
            if(userNotification.notification.notificationName == 'VehicleStatusUpdate'){
                this.vehicles.forEach(vehicle => {
                    if(vehicle.id == userNotification.notification.data.vehicleId){
                        vehicle.lastStatusUpdate = moment(userNotification.notification.data.eventTime);
                        
                        if(this.showOnline != ''){
                            if(this.showOnline == this.isOnline(vehicle).toString()){
                                if(this.filteredVehicles.indexOf(vehicle) < 0){
                                    this.filteredVehicles.push(vehicle);
                                }
                            }
                            else{
                                var i = this.filteredVehicles.indexOf(vehicle);
                                if(i >= 0){
                                    this.filteredVehicles.splice(i);
                                }
                            }
                        } 
                    }
                });
            }
        });
    }

    protected list(
        request: PagedVehiclesRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {

        request.customer = this.customer;

        this._vehiclesService
            .getAll(request.customer, '', request.skipCount, request.maxResultCount)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: PagedResultDtoOfVehicleDto) => {
                this.vehicles = result.items;
                this.filteredVehicles = [];
                result.items.forEach(vehicle => {
                    if(this.showOnline == '' || this.showOnline == this.isOnline(vehicle).toString()){
                        this.filteredVehicles.push(vehicle);
                    }
                });

                this.vehicles.forEach(vehicle => {
                    if(this.customers.indexOf(vehicle.customerName)<0)
                        this.customers.push(vehicle.customerName);
                });

                this.showPaging(result, pageNumber);
            });
    }

    protected delete(entity: VehicleDto): void {
        throw new Error("Method not implemented.");
    }

    public isOnline(vehicle: VehicleDto): boolean {
        if(vehicle.lastStatusUpdate)
        {
            if(vehicle.lastStatusUpdate > moment().add(-1, 'minutes'))
            {
                return true;
            } else {
                return false;
            }
        }
        else {
            return false;
        }
    }
}
