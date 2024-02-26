import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WebApiService {
  private readonly userController = '/User';
  constructor(private http: HttpClient) { }

  public getUsers(callback: Function, errorCb?: Function) {
    this.http.get<User[]>(this.userController + '/GetAllUsers').subscribe(
      (result) => {
        callback?.(result);
      },
      (error) => {
        console.error(error);
        errorCb?.(error)
      }
    );
  }

  addUser(user: User, callback: Function, errorCb?: Function) {
    this.updateUser({ ...user, userId: 0 }, callback, errorCb)
  }

  updateUser(user: User, callback: Function, errorCb?: Function) {
    this.http.post<User>(this.userController + '/UpdateUser', user).subscribe(
      () => {
        callback?.();
      },
      (error) => {
        console.error(error);
        errorCb?.(error)
      }
    )
  }

  deleteUser(userId: number, callback: Function, errorCb?: Function) {
    this.http.get<number>(this.userController + '/DeleteUser', { params: { userId } }).subscribe(
      () => {
        callback?.();
      },
      (error) => {
        console.error(error);
        errorCb?.(error)
      }
    );
  }

  canUseEmail(email: string, userId: number): Observable<boolean> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<boolean>(this.userController + '/IsEmailUsedByAnotherUser', JSON.stringify({ email, userId }), { headers });
  }
}
