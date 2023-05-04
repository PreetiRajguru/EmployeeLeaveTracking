const storageKey = 'token';
const storageKeyRole = 'role';

const saveUser = (user: any) => {
    localStorage.setItem(storageKey, JSON.stringify(user));
}

const loadUser = () => {
    localStorage.getItem(storageKey);
}

const removeUser = () => {
    localStorage.removeItem(storageKey);
}

const saveUserRole = (user: any) => {
    localStorage.setItem(storageKeyRole, JSON.stringify(user));
}

const loadUserRole = () => {
    localStorage.getItem(storageKeyRole);
}

const storage = { saveUser, loadUser, removeUser, saveUserRole, loadUserRole };

export default storage;