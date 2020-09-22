import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private toastr: ToastrService) { }

  showSuccess(message, title) {
    this.toastr.success(message, title)
  }

  showAlert(message, title) {
    this.toastr.warning(message, title);
  }

  showError(message, title) {
    this.toastr.error(message, title)
  }

  showSuccessWithTimeout(message, title, timespan){
    this.toastr.success(message, title ,{
      timeOut : timespan
    })
  }

  showHTMLMessage(message, title){
    this.toastr.success(message, title, {
      enableHtml : true
    })
  }

}
