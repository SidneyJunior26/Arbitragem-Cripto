import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigAdmComponent } from './config-adm.component';

describe('ConfigAdmComponent', () => {
  let component: ConfigAdmComponent;
  let fixture: ComponentFixture<ConfigAdmComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ConfigAdmComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConfigAdmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
