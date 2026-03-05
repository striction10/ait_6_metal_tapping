import Header from '../../components/Header/Header'

function Tasks () {
    return (
        <>
            <Header 
                title="Задания"
                showNav={true}
                showUserBtn={true}
                activeNav = "tasks"
            />
        </>
    );
}

export default Tasks