import React, {useState} from 'react'
import BerkeleySlidMenu from '../../components/SlideMenu';


const BerkeleyLoginPage = () => {
    const [count, setCount] = useState(0);


    return (<div className="w-90 mx-auto">
        <div className="navbar bg-purple-700">
            <div className="">
                <div className="p-6 bg-contain bg-center bg-no-repeat" style={{
                    backgroundImage: "url(" + "/img/berkeley/navigation-logo-white-60.png" + ")",
                    width: "150px",
                    cursor: "pointer"
                }}></div>
            </div>
            <div className="text-white mx-auto text-2xl tracking-wider">Dash board</div>
        </div>
        <div className="flex flex-row">
            <BerkeleySlidMenu/>
           
        </div>
    </div>)
}


export default BerkeleyLoginPage;