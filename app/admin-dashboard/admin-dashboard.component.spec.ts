import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { DbAccessService } from '../db-access.service';
import { AdminDashboardComponent } from './admin-dashboard.component';

describe('AdminDashboardComponent', () => {
  let component: AdminDashboardComponent;
  let fixture: ComponentFixture<AdminDashboardComponent>;
  let routerSpy = { navigate: jasmine.createSpy('navigate') };
  let dbAccessServiceSpy: jasmine.SpyObj<DbAccessService>;

  beforeEach(() => {
    const spy = jasmine.createSpyObj('DbAccessService', ['GetAllUsers', 'GetAllProjects', 'DeleteUser', 'DeleteProject']);

    TestBed.configureTestingModule({
      declarations: [AdminDashboardComponent],
      providers: [
        { provide: Router, useValue: routerSpy },
        { provide: DbAccessService, useValue: spy }
      ]
    });

    fixture = TestBed.createComponent(AdminDashboardComponent);
    component = fixture.componentInstance;
    dbAccessServiceSpy = TestBed.inject(DbAccessService) as jasmine.SpyObj<DbAccessService>;

    dbAccessServiceSpy.GetAllUsers.and.returnValue(of([]));
    dbAccessServiceSpy.GetAllProjects.and.returnValue(of([]));
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should navigate to add new user', () => {
    component.NavigateToAddNewUser();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['addnewuser']);
  });

  it('should navigate to add new project', () => {
    component.NavigateToAddNewProject();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['addnewproject']);
  });

  it('should load users on init', () => {
    expect(dbAccessServiceSpy.GetAllUsers).toHaveBeenCalled();
  });

  it('should load projects on init', () => {
    expect(dbAccessServiceSpy.GetAllProjects).toHaveBeenCalled();
  });

  it('should delete user', () => {
    dbAccessServiceSpy.DeleteUser.and.returnValue(of({}));
    component.deleteUser('1');
    expect(dbAccessServiceSpy.DeleteUser).toHaveBeenCalledWith('1');
    expect(dbAccessServiceSpy.GetAllUsers).toHaveBeenCalled();
  });

  it('should delete project', () => {
    dbAccessServiceSpy.DeleteProject.and.returnValue(of({}));
    component.deleteProject('1');
    expect(dbAccessServiceSpy.DeleteProject).toHaveBeenCalledWith('1');
    expect(dbAccessServiceSpy.GetAllProjects).toHaveBeenCalled();
  });
});