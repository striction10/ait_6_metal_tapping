import "./Header.css"
import logo from '../../assets/rusalLogoGrey.svg'

function Header() {
    return (
        <div id="header">
            <img src={logo} id="logo1" alt="RUSAL logo" />
            <h1>Выливка металла</h1>
        </div>
    )
}

export default Header