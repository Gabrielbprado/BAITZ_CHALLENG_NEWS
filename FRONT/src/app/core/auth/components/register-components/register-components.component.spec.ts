import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterComponentsComponent } from './register-components.component';

describe('RegisterComponentsComponent', () => {
  let component: RegisterComponentsComponent;
  let fixture: ComponentFixture<RegisterComponentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterComponentsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterComponentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
