import { Component, OnInit } from '@angular/core';
import { Discount } from '../../../data/models/domain/discount';
import { DiscountService } from 'src/app/services/discount.service';

@Component({
  selector: 'app-discounts-page',
  templateUrl: './discounts-page.component.html',
  styleUrls: ['./discounts-page.component.scss']
})
export class DiscountsPageComponent implements OnInit {
  discounts: Discount[] = [];
  constructor(private discountService: DiscountService) { }

  ngOnInit() {

  }

}