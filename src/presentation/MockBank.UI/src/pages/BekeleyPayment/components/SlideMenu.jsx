import React from 'react';
import { BeakerIcon } from '@heroicons/react/solid'
import TableData from "@/pages/BekeleyPayment/components/TableData";

const BerkeleySlidMenu = ()=>{
    const NavigationItem = [{icon:'', link:'', label:'Dashboard', child:[]},
    {icon:'', link:'', label:'Direct Send', child:[
        {icon:'', link:'', label:'Direct Send'}
        ]},
    {icon:'', link:'', label:'Dashboard', child:[
        {icon:'', link:'', label:'Create a Transfer'},
        {icon:'', link:'', label:'Transfer History'},
        {icon:'', link:'', label:'Account Holders'},
        {icon:'', link:'', label:'Reporting'},
        {icon:'', link:'', label:'Limits'}
    ]} ,
    {icon:'', link:'', label:'API', child:[
        {icon:'', link:'', label:'Create a Transfer'},
        {icon:'', link:'', label:'Transfer History'},
        {icon:'', link:'', label:'Account Holders'},
        {icon:'', link:'', label:'Reporting'},
        {icon:'', link:'', label:'Limits'}
    ]} ,
   {icon:'', link:'', label:'Setting', child:[
        {icon:'', link:'', label:'Create a Transfer'},
        {icon:'', link:'', label:'Transfer History'},
        {icon:'', link:'', label:'Account Holders'},
        {icon:'', link:'', label:'Reporting'},
        {icon:'', link:'', label:'Limits'}
    ]} ,
   ]

return (<div className="drawer drawer-mobile">
    <input id="my-drawer-2" type="checkbox" className="drawer-toggle" />
    <div className="drawer-content">
        <TableData/>
    </div> 
    <div className="drawer-side">
      <label for="my-drawer-2" className="drawer-overlay"></label> 
      <ul className="menu p-4 overflow-y-auto  text-base-content my-5">
         {NavigationItem.map(item => (<li className="text-2xl bg-white hover:bg-gray-300 w-80 tracking-wider my-2" style={{cursor: "pointer"}}>   
             {item.label}</li>))}      
      </ul>
    
    </div>
  </div>)
}

export default BerkeleySlidMenu