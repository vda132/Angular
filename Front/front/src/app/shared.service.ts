import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})

export class SharedService {
  readonly APIUrl="https://localhost:7049/api";

  constructor(private http: HttpClient) { }

  getTasks() : Observable<any> {
    return this.http.get(this.APIUrl + '/Tasks');
  }

  postTask(task:any) : Observable<any> {
    return this.http.post(this.APIUrl + '/Tasks', task);
  }

  putTask(id:any, task:any) {
    return this.http.put(this.APIUrl+ '/Tasks/' + id, task);
  }

  deleteTask(id:any) {
    console.log(this.APIUrl + '/Tasks/' + id)
    return this.http.delete(this.APIUrl + '/Tasks/' + id);
  }
}
